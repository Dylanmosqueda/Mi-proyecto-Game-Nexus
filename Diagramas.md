Nivel 1
###  ¿Para quién es?
* **Audiencia:** Miembros no técnicos del equipo, patrocinadores (stakeholders), clientes y nuevos desarrolladores que se incorporan al proyecto.

###  ¿Qué pregunta responde?
* **Pregunta:** ¿Cuál es el alcance del sistema Game Nexus, quiénes son sus usuarios y con qué sistemas externos interactúa?

```mermaid
flowchart TB
    %% Elementos de Usuario
    Gamer[" Gamer / Usuario<br/>(Busca juegos y gestiona reseñas)"]
    Admin[" Administrador<br/>(Modera contenido y gestiona catálogo)"]
    
    %% Sistema Principal
    subgraph SystemBoundary [Límite del Sistema]
        GameNexus[" Sistema Game Nexus<br/>(Permite a los usuarios organizar su catálogo y escribir reseñas)"]
    end
    
    %% Sistemas Externos
    ExtAPI[" API Externa de Juegos<br/>(RAWG / IGDB para datos de videojuegos)"]
    EmailService[" Servicio de Correo<br/>(Envío de notificaciones y confirmaciones)"]

    %% Relaciones
    Gamer -->|Explora juegos y escribe reseñas usando| GameNexus
    Admin -->|Administra catálogo y usuarios en| GameNexus
    GameNexus -->|Sincroniza información de catálogos desde| ExtAPI
    GameNexus -->|Envía notificaciones de registro a través de| EmailService

    %% Estilos de C4
    classDef actor fill:#08427B,stroke:#052E56,color:#FFFFFF;
    classDef system fill:#1168BD,stroke:#0B4E8F,color:#FFFFFF;
    classDef external fill:#999999,stroke:#666666,color:#FFFFFF;
    
    class Gamer,Admin actor;
    class GameNexus system;
    class ExtAPI,EmailService external;
```
