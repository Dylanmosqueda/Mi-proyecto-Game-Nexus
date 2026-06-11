# ADR-01: [Título corto de la decisión]

| Campo  | Valor |
|--------|-------|
| Autor  | [Dylan Emmauel Mosqueda Lugo] |
| Fecha  | 11/06/2026 |
| Estado | `Propuesto`|

---

## Contexto

Estoy construyendo GameNexus Hub, una plataforma híbrida para la gestión de bibliotecas de videojuegos y repositorios de activos técnicos (assets). 
El sistema debe permitir a desarrolladores independientes y modders vincular archivos binarios (.fbx, .wav, .java) a títulos específicos de su colección.
Las restricciones incluyen el uso de Java como lenguaje principal y el cumplimiento de un cronograma académico que exige una estructura modular. Se requiere una solución que permita manejar tanto metadatos (texto) como archivos físicos de forma organizada sin que el código se vuelva inmanejable.

---

## Decisión
He decidido implementar una Arquitectura en Capas (N-Tier Architecture) siguiendo el patrón MVC (Modelo-Vista-Controlador).
La estructura se dividirá en:
Capa de Presentación (View): Interfaz de usuario para visualizar el catálogo y gestionar archivos.
Capa de Lógica de Negocio (Controller/Service): Gestión de validaciones, versionado de assets y procesamiento de metadatos.
Capa de Acceso a Datos (Model/Persistence): Manejo de la base de datos relacional y el sistema de archivos local.
### ¿Por qué?
Esta decisión resuelve el problema de la fragmentación de información al permitir la Separación de Responsabilidades. Al separar la lógica de negocio de la persistencia, puedo modificar la forma en que se guardan los archivos físicos (assets) sin afectar la interfaz del usuario.
Java se beneficia enormemente de este estilo debido a su naturaleza orientada a objetos, lo que facilita el uso de interfaces para desacoplar los componentes y asegurar que el flujo de datos entre la biblioteca y el repositorio de activos sea predecible y seguro.
### Alternativas consideradas

*(Mínimo 3 filas)*

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| Microservicios| Añade una complejidad excesiva de orquestación y red que no se justifica para una herramienta de gestión local.|
| Arquitectura Hexagonal| Aunque ofrece un desacoplamiento superior, introduce una complejidad inicial muy alta con exceso de código "boilerplate" (interfaces y adaptadores) que ralentizaría el desarrollo del MVP sin aportar un beneficio crítico inmediato.|
| Arquitectura Serverless| No es viable para el manejo de activos binarios de gran tamaño (modelos 3D, texturas 4K) debido a las limitaciones de tiempo de ejecución de las funciones nube y los altos costos asociados al almacenamiento y transferencia de datos binarios.|
| Arquitectura Basada en Eventos|Introduce una dificultad significativa en la trazabilidad y depuración. Un gestor de activos requiere una consistencia de datos inmediata y secuencial, la cual es difícil de garantizar en un entorno puramente asíncrono basado en eventos.|
| Arquitectura Cliente-Servidor| Al ser un proyecto enfocado en la gestión local de archivos para modders, un modelo puramente cliente-servidor añadiría una dependencia obligatoria de conexión a internet, limitando el uso de la herramienta en entornos de desarrollo offline.|

---

## Consecuencias

**✅ Lo que gano:**

Consecuencia Técnica: El sistema se vuelve mantenible y escalable. Es fácil sustituir la base de datos local por una en la nube en el futuro sin reescribir la lógica central.
Consecuencia sobre el proceso: Facilita la documentación y el orden del código, permitiendo revisiones de software más claras al saber exactamente dónde reside cada funcionalidad.

**⚠️ Lo que sacrifico o asumo:**

Limitación técnica: Existe una mayor verbosidad inicial; se deben crear más clases y estructuras de carpetas antes de ver resultados visuales.
Deuda o riesgo: Si no se respetan estrictamente los límites de las capas, se puede caer en un "Monolito Degenerado", donde las capas dependen demasiado entre sí.

## Diagrama

Un boceto de cómo se estructura tu sistema (draw.io, Mermaid o a mano escaneado)

![Diagrama del sistema]( ./ruta/diagrama-nivel-1.png )
