# ADR-05: deudas-tecnicas


| Campo  | Valor |
|--------|-------|
| Autor  | Dylan Emmanuel Mosqueda Lugo |
| Fecha  | 18/06/2026 |
| Estado | `Propuesto`|

---

## Contexto

Durante las fases iniciales de desarrollo de **Game Nexus**, priorizamos la entrega rápida de características funcionales en la API (`Game Nexus.API`) y la integración del cliente. Para cumplir con los plazos estimados, tomamos decisiones de diseño y configuración simplificadas. Con el fin de evitar que estas decisiones comprometan la mantenibilidad y seguridad del proyecto a largo plazo, procedemos a documentar estas deudas técnicas para planificar su resolución.

---

## Decisión

Se ha tomado la decisión de **migrar a una base de datos relacional externa hospedada en AWS RDS (Relational Database Service)** y descartar definitivamente el uso de archivos JSON locales en la máquina virtual de EC2. 

### ¿Por qué?

La API accederá a sus datos en producción mediante el ORM **Entity Framework Core (EF Core)**, utilizando una cadena de conexión segura provista a través de variables de entorno de AWS (como AWS Secrets Manager) para evitar exponer credenciales en el código fuente.

### Alternativas consideradas

*(Mínimo 3 filas)*

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| Archivos JSON en EC2| El almacenamiento de las instancias EC2 es efímero. Si la instancia se detiene o termina, todos los archivos JSON con la información de los videojuegos y reseñas se perderían permanentemente.|
| SQLite local en EC2 | Aunque es más robusto que un JSON, sigue dependiendo del disco local de la instancia. Esto impide la escalabilidad, ya que no podríamos tener dos servidores de API compartiendo la misma base de datos.|
| AWS DynamoDB (NoSQL)| Aunque es excelente para escalabilidad, nuestro modelo de datos requiere relaciones claras (una Reseña pertenece a un Videojuego). Un modelo relacional (SQL) se ajusta mejor a la integridad referencial necesaria.|

---

## Consecuencias

**✅ Lo que gano:**

**Técnica**: **Persistencia Independiente.** La base de datos vive fuera del servidor web. Si el servidor (EC2) falla, los datos están seguros en RDS. Además, ganamos integridad referencial (no se pueden crear reseñas de juegos que no existen).
**Proceso/Equipo**: **Estandarización.** Al usar EF Core y RDS, el proceso de actualización de la base de datos se vuelve automático mediante "Migrations", permitiendo que cualquier miembro del equipo replique la estructura exacta de la base de datos fácilmente.

**⚠️ Lo que sacrifico o asumo:**

**Limitación técnica**: **Latencia de Red.** Al estar la base de datos en un servicio externo, cada consulta tiene un pequeño retraso adicional de red comparado con leer de la memoria local, lo que requiere optimizar las consultas.
**Deuda o riesgo**: **Costo de Infraestructura.** AWS RDS tiene un costo superior al almacenamiento local. Se asume el riesgo de salir de la capa gratuita si no se monitorea correctamente el tamaño de la instancia de base de datos.

### Deuda Técnica 1: Gestión de Configuración e Infraestructura
*   **Qué es:** 
    Uso de credenciales de infraestructura (cadena de conexión a SQL Server, claves JWT para autenticación de usuarios y credenciales de APIs de terceros como IGDB o Steam) quemadas directamente en el archivo `appsettings.development.json` o expuestas en el código de inicialización en `Program.cs`. No existe una separación clara de secretos por entorno.
*   **Por qué existe:** 
    Fue una decisión consciente para acelerar el arranque del entorno de desarrollo local y simplificar la colaboración inicial entre los miembros del equipo, evitando la sobrecarga de configurar bóvedas de claves externas en etapas tempranas.
*   **Costo de no pagarla:** 
    Si el proyecto escala o se prepara para producción, corremos un riesgo elevado de fuga de credenciales sensibles si un desarrollador sube por error estos archivos al repositorio público de Git. Además, dificulta el despliegue automatizado (CI/CD), ya que los entornos de pruebas y producción requerirían modificar manualmente archivos físicos de configuración en el servidor.
*   **Propuesta de solución:** 
    Aplicar la técnica de **Externalización de Configuración**. Utilizaremos el Administrador de Secretos de usuario de .NET (`dotnet user-secrets`) para el desarrollo local, excluyendo por completo los secretos del control de versiones. Para entornos de producción, se migrará la infraestructura hacia variables de entorno del sistema o un servicio de bóveda gestionado (como Azure Key Vault o AWS Secrets Manager), mapeando estas propiedades de forma segura mediante el patrón de opciones fuertemente tipadas (`IOptions<TOptions>`) en .NET.



### Deuda Técnica 2: Arquitectura de Software (Controladores Acoplados / "Fat Controllers")
*   **Qué es:** 
    Inyección directa de la clase `DbContext` de Entity Framework Core dentro de los controladores de la API (`GameController.cs` y `ReviewController.cs`), procesando la lógica de negocio, validaciones de dominio (como el cálculo de promedios de calificación o validaciones de usuarios) y el formateo de respuestas directamente en los endpoints.
*   **Por qué existe:** 
    Descuido no detectado a tiempo debido a la inercia del desarrollo inicial. Al buscar que la API devolviera datos rápidamente para que el cliente frontend pudiera consumirlos, se omitió la creación de una capa de servicios o de lógica de negocio independiente.
*   **Costo de no pagarla:** 
    A medida que agreguemos más reglas de negocio, los controladores se volverán gigantescos y difíciles de mantener (violando el Principio de Responsabilidad Única). Adicionalmente, escribir pruebas unitarias automatizadas se volverá sumamente complejo, ya que nos obligará a simular (`mockear`) el comportamiento completo de la base de datos y de la infraestructura HTTP en lugar de probar la pura lógica de negocio.
*   **Propuesta de solución:** 
    Aplicar la refactorización mediante la **Separación en Capas** o el **Patrón Mediator** (usando la biblioteca MediatR). Extraeremos la lógica de persistencia de datos y de negocio de los controladores hacia manejadores de peticiones independientes (Handlers) o Servicios de Aplicación. Los controladores solo actuarán como una capa delgada de entrada y salida, delegando el procesamiento real a estas clases desacopladas.


## Diagrama

<img width="5163" height="1673" alt="Untitled diagram-2026-06-18-222448" src="https://github.com/user-attachments/assets/586ac695-e412-459e-9dbc-f4d1f73833e9" />

