# ADR-01: GameNexus Hub

| Campo  | Valor |
|--------|-------|
| Autor  | Dylan Emmanuel Mosqueda Lugo |
| Fecha  | 13/05/2026 |
| Estado | `Propuesto` |

---

## Contexto
Estoy construyendo GameNexus Hub, una solución integral desarrollada sobre la plataforma .NET. El sistema funciona como un gestor de biblioteca y un repositorio técnico de activos (assets) para desarrolladores y modders.
El problema central es la fragmentación de metadatos y archivos binarios en el desarrollo de videojuegos independiente.

Para desarrolladores que utilizan el ecosistema de Microsoft y necesitan una herramienta centralizada para mapear sus títulos con sus recursos de ingeniería.

Restricciones:
Entorno: Uso obligatorio de Visual Studio Enterprise/Community y el SDK de .NET.
Tiempo: Necesidad de un desarrollo ágil mediante Scaffolding y Entity Framework Core.
Tecnologías conocidas: Dominio de C#, Programación Orientada a Objetos (POO) y el manejo de paquetes a través de NuGet.


---

## Decisión

He decidido implementar .NET 

### ¿Por qué?
por que es mas facil de aprender y usar, 
ademas de ser el entorno de desarrollo estándar visto en clase y el que ofrece mayor robustez para el manejo de tipos de datos complejos.


### Alternativas consideradas

*(Mínimo 3 filas)*

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| ASP.NET Core Minimal APIs|Aunque son rápidas, carecen de la estructura formal de carpetas y controladores que requiere un sistema con lógica de negocio compleja como el manejo de versiones de assets. |
| Arquitectura de Microservicios (.NET + Docker)|La sobrecarga de orquestación y comunicación vía HTTP/gRPC superaba el alcance del proyecto y añadía latencia innecesaria para un MVP.                  |
| Patrón Monolítico Simple|Descartado porque mezclar el acceso a datos con la interfaz en un solo proyecto de C# dificulta las pruebas unitarias (Unit Testing) y el mantenimiento a largo plazo.                |

---

## Consecuencias

**✅ Lo que gano:**

Técnica: Gracias al uso de Entity Framework Core, gano el control total de la base de datos mediante Migrations, lo que facilita la evolución del esquema de datos (juegos y assets) de forma segura y versionada.
Proceso/Equipo: El uso de estándares de .NET (como las interfaces y los DTOs) permite que el código sea altamente legible y profesional, 
facilitando la integración de otros desarrolladores que conozcan las convenciones de C#.
**⚠️ Lo que sacrifico o asumo:**

Menciona al menos:
Limitación técnica: El Overhead inicial de .NET. Al ser un framework robusto,
el consumo de memoria inicial y el tamaño de los binarios es mayor que en lenguajes de bajo nivel o scripts simples.
Deuda o riesgo: La dependencia de librerías externas vía NuGet. Si una dependencia clave deja de actualizarse o tiene vulnerabilidades,
el sistema podría requerir una refactorización mayor en la capa de infraestructura.

## Diagrama

Un boceto de cómo se estructura tu sistema (draw.io, Mermaid o a mano escaneado)

![Diagrama del sistema]( ./ruta/diagrama-nivel-1.png )
