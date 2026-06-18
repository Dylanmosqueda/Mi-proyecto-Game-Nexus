# ADR-04: Incorporación de la API REST


| Campo  | Valor |
|--------|-------|
| Autor  | Dylan Emmanuel Mosqueda Lugo |
| Fecha  | 18/06/2026 |
| Estado | `Propuesto`|

---

## Contexto

Actualmente, la API REST del proyecto Game Nexus se ejecuta localmente utilizando un almacenamiento simulado en memoria para facilitar el desarrollo rápido de las entidades `Item` y `Review`. 
No obstante, al planificar el despliegue de la solución a un entorno de producción en AWS (Amazon Web Services), se requiere definir un mecanismo de almacenamiento persistente que garantice la disponibilidad, consistencia y seguridad de la información.

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

## Diagrama

<img width="5163" height="1673" alt="Untitled diagram-2026-06-18-222448" src="https://github.com/user-attachments/assets/586ac695-e412-459e-9dbc-f4d1f73833e9" />

