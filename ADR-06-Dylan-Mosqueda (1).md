# ADR-06: Integracion de xUnit en Game Nexus Hub

| Campo  | Valor |
|--------|-------|
| Autor  | Dylan Emmanuel Mosqueda Lugo |
| Fecha  | 24/06/2026 |
| Estado | `Reemplazado por ADR 4-Dylan` |

---

## Contexto

En el proyecto **Game Nexus Hub** (un catálogo y plataforma de reseñas de videojuegos), los controladores (tanto el controlador MVC clásico como los controladores de la API REST) 
necesitan acceder de forma consistente y unificada a la fuente de datos en memoria para evitar inconsistencias en el inventario. 

Adicionalmente, se identificó la necesidad de ofrecer opciones de ordenamiento dinámico del catálogo de videojuegos (por orden alfabético o de mejor a peor calificación). Implementar esto directamente con bloques condicionales (`switch` o `if/else`) dentro del servicio principal o de los controladores violaría el principio de Responsabilidad Única y el principio de Abierto/Cerrado (Solid). La arquitectura debe ser modular, limpia y flexible,
considerando la restricción de tiempo académico y la importancia de estructurar el código sin acoplamiento antes de migrar a una base de datos en producción.

---

## Decisión

Se decidió integrar dos patrones de diseño de la Pandilla de los Cuatro (GoF):
1.  **Patrón Singleton (Creacional)**: Para gestionar el ciclo de vida de la clase de acceso a datos en memoria (`GameNexusDb`), asegurando una instancia única global.
2.  **Patrón Strategy (De comportamiento)**: Para abstraer y encapsular los algoritmos de ordenamiento del catálogo mediante una interfaz común (`ISortingStrategy`) y clases de estrategia concretas (`SortByTitleStrategy` y `SortByCalificacionStrategy`).

### ¿Por qué?

*   **Singleton**: Evita que los diferentes servicios o controladores instancien bases de datos independientes. Al implementar un Singleton utilizando la clase `Lazy<T>` de .NET, garantizamos que el almacén de datos se inicialice solo cuando sea necesario (*lazy loading*) y que sea completamente seguro frente a accesos concurrentes (*thread-safe*), todo sin la complejidad de registrar servicios adicionales o instanciar objetos globales de forma insegura.
*   **Strategy**: Resuelve el problema de la rigidez en el ordenamiento. En lugar de modificar los métodos internos del servicio `ItemService` cada vez que el usuario desee un tipo de ordenamiento diferente, el servicio simplemente recibe y ejecuta un contrato `ISortingStrategy`. Esto permite cambiar el comportamiento de ordenamiento en tiempo de ejecución de manera limpia y transparente.

### Alternativas consideradas

*(Mínimo 3 filas)*

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| Patrón Template Method (GoF)| Requiere una jerarquía de herencia rígida (subclases de ItemService) en lugar de composición, lo que generaría un exceso de clases acopladas para cada tipo de ordenamiento.          |
| LINQ en los Controladores   | Mezclaría la lógica de negocio y de ordenamiento dentro de los controladores HTTP, violando el principio de Responsabilidad Única (SRP) y dificultando el mantenimiento y las pruebas.|
| Factory Method (GoF)        | Es un patrón creacional enfocado en cómo construir objetos, no en cómo intercambiar el comportamiento de un algoritmo en tiempo de ejecución como lo hace Strategy.                   |

---

## Consecuencias

**✅ Lo que gano:**

*   **Consecuencia técnica**: Cumplimiento del principio de Abierto/Cerrado (OCP). Si en el futuro se requiere añadir un nuevo criterio de ordenamiento (por ejemplo, ordenar por consola o por año de lanzamiento), solo será necesario crear una nueva clase que implemente `ISortingStrategy`, sin tener que modificar la lógica existente del servicio `ItemService` ni el código del controlador.
*   **Consecuencia sobre el proceso o el equipo**: Se reduce la posibilidad de generar conflictos de fusión (*merge conflicts*) en sistemas de control de versiones. Al estar los algoritmos de ordenamiento aislados en archivos separados (estrategias independientes), diferentes desarrolladores pueden programar nuevos criterios de orden de manera paralela sin alterar el archivo de servicio común.

**⚠️ Lo que sacrifico o asumo:**

*   **Limitación técnica**: Incremento en el número total de clases pequeñas dentro del proyecto (clases de estrategia específicas como `SortByTitleStrategy` y `SortByCalificacionStrategy`). Esto añade una sobrecarga de navegación en la estructura de carpetas de la solución si la cantidad de estrategias crece significativamente.
*   **Deuda o riesgo**: El uso del Singleton en memoria (`GameNexusDb`) asume el riesgo de pérdida de datos ante un reinicio del servidor IIS o de la aplicación. Además, restringe la escalabilidad horizontal en producción (múltiples servidores tendrían estados diferentes), una deuda técnica conocida que deberá ser saldada en el futuro mediante la migración a una base de datos externa administrada (AWS RDS / MongoDB Atlas).


## Diagrama
<img width="1240" height="1675" alt="Untitled diagram-2026-06-25-004252" src="https://github.com/user-attachments/assets/95f2f515-3ed5-4cbe-b263-3720bc098fae" />
<img width="1590" height="2850" alt="Gamer API Integration-2026-06-25-004315" src="https://github.com/user-attachments/assets/8797f76f-1a22-4173-b4bd-7b67a658a6f4" />
<img width="8192" height="3360" alt="ItemsController Strategy-2026-06-25-004343" src="https://github.com/user-attachments/assets/1cb8397f-e40f-4b59-b000-04f0579b25ee" />

---

## Adición: Estrategia de Pruebas Unitarias (xUnit)

Para garantizar la estabilidad del sistema durante futuros cambios y refactorizaciones, hemos seleccionado tres componentes clave para implementar pruebas unitarias automatizadas:

1. **`RatingCalculator` (Lógica de Cálculo / Utilidad):**
   * *Por qué se eligió:* Al ser una clase que realiza operaciones matemáticas sobre datos provistos por los usuarios (calificaciones), es susceptible a errores en tiempo de ejecución (como divisiones por cero o manejo de listas nulas). Las pruebas aseguran que el motor de evaluación de videojuegos sea robusto.
2. **`Game` (Entidad de Dominio / Reglas de Validación):**
   * *Por qué se eligió:* Representa la entidad principal del negocio. Validar que las propiedades básicas de un juego cumplan con las reglas de negocio antes de ser persistidas evita la propagación de datos corruptos hacia la base de datos.
3. **`TokenService` (Infraestructura / Seguridad):**
   * *Por qué se eligió:* Este componente maneja la autenticación. Es crítico asegurar que las claves de cifrado cumplan con los estándares de seguridad requeridos por los algoritmos de hashing antes de levantar el servidor de producción.

Estas pruebas siguen estrictamente la estructura **Arrange-Act-Assert (AAA)** para facilitar la lectura y el mantenimiento de las especificaciones de software.



