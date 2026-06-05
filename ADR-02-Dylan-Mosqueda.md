# ADR-02: Definición y Selección de Vistas Arquitectónicas del Sistema (Game Nexus)

| Campo  | Valor |
|--------|-------|
| Autor  | [Dylan Emmanuel Mosqueda Lugo] |
| Fecha  | 04/06/2026 |
| Estado | `Propuesto`|

---

## Contexto
El desarrollo de la plataforma Game Nexus requiere una base documental clara que describa su estructura de software.
Para facilitar la comprensión técnica, guiar la implementación por parte del equipo y asegurar que los requerimientos de escalabilidad y rendimiento se cumplan,
es necesario documentar el sistema bajo diferentes perspectivas.

---

## Decisión

Se decidió adoptar el Modelo de Vistas Arquitectónicas. Este modelo se compone de varias vistas que describen diferentes aspectos del sistema,
como la vista lógica, la vista de desarrollo, la vista de proceso y la vista física.

Específicamente, se modelan cuatro vistas clave adaptadas al estilo arquitectónico Cliente-Servidor (Full-Stack Decoupled) de Game Nexus:

Vista Lógica: Representa la estructura de capas de software (Presentación, Aplicación, Dominio y Datos).

Vista Física (Componentes): Estructura de directorios y organización de módulos en el código fuente.

Vista de Despliegue: Distribución de los servicios e infraestructura en la nube (proveedores como Vercel/Render/Supabase).

Vista de Procesos: El comportamiento dinámico del sistema para el flujo crítico de consulta e integración de la API externa de videojuegos.



### ¿Por qué?

He decidido adoptar el modelo de vistas arquitectónicas para estructurar y documentar el diseño del sistema "Game Nexus Hub". 
Nos enfocaremos en cuatro vistas principales, representadas a través de diagramas en formato Mermaid.js para facilitar su mantenimiento directo dentro del control de versiones en GitHub:

1. **Vista Lógica:** Describe la organización en capas del software y las responsabilidades de sus componentes.
2. **Vista Física:** Detalla los componentes de hardware, los dispositivos físicos y los canales de comunicación involucrados.
3. **Vista de Despliegue:** Muestra cómo los artefactos de software mapean y se ejecutan en entornos virtuales y de nube (PaaS/SaaS).
4. **Vista de Procesos:** Ilustra la interacción dinámica y secuencial entre los distintos componentes para cumplir un caso de uso representativo del sistema.


### Alternativas consideradas

*(Mínimo 3 filas)*

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| Arquitectura Monolítica sin Vistas| Aunque es más rápida de implementar inicialmente, se descartó porque acopla fuertemente el frontend con el backend, dificultando la escalabilidad independiente y la integración limpia con APIs externas de videojuegos.|
| Modelado exclusivo con Diagramas C4| Aunque el modelo C4 es excelente para representar niveles de abstracción (Contexto, Contenedores, Componentes, Código),se descartó para este hito debido a que las pautas del proyecto exigen la formalidad del estándar de 4 vistas tradicionales para mapear explícitamente aspectos de red, despliegue físico e interacción de procesos secuenciales.|
| Desarrollo sin Documentación de Vistas (Ad-Hoc)| Se descartó por completo debido al alto riesgo de generar deuda técnica acumulada,malentendidos en la distribución de responsabilidades del equipo y falta de claridad sobre dónde deben residir lógicamente las llamadas a la API de RAWG/IGDB frente a la base de datos local.|

---

## Consecuencias

**✅ Lo que gano:**

* **Consecuencia Técnica:** Un desacoplamiento total entre la interfaz de usuario (React) y la lógica de negocio (Backend API). 
* Esto permite que el diseño de las vistas de Game Nexus pueda evolucionar o rediseñarse por completo sin alterar la base de datos o los endpoints del servidor.
* **Consecuencia sobre el Proceso/Equipo:** Una guía visual clara y estandarizada que agiliza el flujo de trabajo del equipo de desarrollo, reduciendo la fricción y facilitando la distribución de tareas durante la implementación de nuevas funcionalidades.

**⚠️ Lo que sacrifico o asumo:**

* **Limitación Técnica:** Una ligera sobrecarga de latencia y manejo de errores debido a que cada interacción principal requiere llamadas a través de la red (HTTPS/JSON) entre el cliente, el backend y la API de videojuegos de terceros.
* **Deuda o Riesgo:** El compromiso de mantener actualizados estos 4 diagramas a medida que el sistema incorpore nuevas dependencias o sufra cambios en sus rutas de archivos, asumiendo el riesgo de desfase documental si no se revisa en cada ciclo de entrega.

## Diagramas 

 1. Vista Lógica (Estructura de Capas)
Muestra la organización del código dividida en responsabilidades lógicas independientes.

2. Vista Física (Estructura de Componentes y Directorios)
Representa la organización de carpetas y módulos dentro del código fuente del proyecto.

3. Vista de Despliegue (Infraestructura de Red)
Ilustra la infraestructura física en la nube donde residen, se ejecutan y se comunican los artefactos de software.

4. Vista de Procesos (Flujo de Consulta de Videojuegos)
Describe la secuencia temporal de un proceso crítico: cuando el usuario busca e interactúa con la ficha de un videojuego.



