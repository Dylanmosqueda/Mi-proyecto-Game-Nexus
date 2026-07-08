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

Nivel 2 
## ¿Para quién es?
Audiencia: Desarrolladores, arquitectos de software y personal de operaciones (DevOps).
## ¿Qué pregunta responde?
Pregunta: ¿Cuáles son las aplicaciones o contenedores de datos que componen el sistema, qué tecnologías usan y cómo se comunican entre sí?


```mermaid
flowchart TB
    Gamer[" Gamer / Usuario"]
    Admin[" Administrador"]

    subgraph GameNexusSystem [Contenedores de Game Nexus]
        WebApp[" Aplicación Frontend<br/>[React / Vue o HTML/JS]<br/>Interfaz de usuario responsiva e interactiva"]
        BackendAPI[" API Backend<br/>[Node.js / Python / Java]<br/>Provee lógica de negocio, autenticación y endpoints REST"]
        Database[(" Base de Datos<br/>[PostgreSQL / MySQL / MongoDB]<br/>Guarda usuarios, reseñas y favoritos")]
    end

    ExtAPI[" API Externa de Juegos"]
    EmailService[" Servicio de Correo"]

    %% Relaciones de comunicación
    Gamer -->|Usa| WebApp
    Admin -->|Administra usando| WebApp
    WebApp -->|Realiza peticiones HTTP / JSON a| BackendAPI
    BackendAPI -->|Lee y escribe usando queries/ORM en| Database
    BackendAPI -->|Solicita envío de emails por SMTP/HTTP a| EmailService
    BackendAPI -->|Consulta catálogo de videojuegos a| ExtAPI

    %% Estilos de C4
    classDef actor fill:#08427B,stroke:#052E56,color:#FFFFFF;
    classDef container fill:#438DD5,stroke:#2E6295,color:#FFFFFF;
    classDef db fill:#1168BD,stroke:#0B4E8F,color:#FFFFFF;
    classDef external fill:#999999,stroke:#666666,color:#FFFFFF;

    class Gamer,Admin actor;
    class WebApp,BackendAPI container;
    class Database db;
    class ExtAPI,EmailService external;
```

Nivel 3 — Componentes
## ¿Para quién es?
Audiencia: Desarrolladores de software y líderes técnicos encargados de codificar o dar mantenimiento a esta sección del sistema.
## ¿Qué pregunta responde?
Pregunta: ¿Cómo está estructurado internamente el contenedor principal (API Backend) y cuáles son las responsabilidades de sus componentes?


```mermaid
flowchart TB
    WebApp[" Aplicación Frontend"]
    Database[(" Base de Datos")]
    ExtAPI[" API Externa de Juegos"]

    subgraph BackendAPIContainer [Límite de la API Backend]
        %% Controladores (Controllers)
        AuthController[" Auth Controller<br/>Maneja el registro y login de usuarios"]
        GameController[" Game Controller<br/>Maneja consultas e información de videojuegos"]
        ReviewController[" Review Controller<br/>Maneja operaciones CRUD de las reseñas"]
        
        %% Servicios (Services)
        AuthService[" Auth Service<br/>Valida credenciales y genera tokens (JWT)"]
        GameService[" Game Service<br/>Procesa la información y consume la API externa"]
        ReviewService[" Review Service<br/>Aplica reglas de negocio de las reseñas"]
        
        %% Repositorios / Acceso a datos
        UserRepository[" User Repository / DAO<br/>Mapea el acceso a datos de usuarios"]
        ReviewRepository[" Review Repository / DAO<br/>Mapea el acceso a datos de reseñas"]
    end

    %% Flujo de peticiones desde el exterior
    WebApp -->|HTTP POST /auth/*| AuthController
    WebApp -->|HTTP GET /games/*| GameController
    WebApp -->|HTTP GET-POST-DELETE /reviews/*| ReviewController

    %% Comunicación interna
    AuthController -->|Llama a| AuthService
    GameController -->|Llama a| GameService
    ReviewController -->|Llama a| ReviewService

    AuthService -->|Usa| UserRepository
    GameService -->|Consulta directo o por caché| ExtAPI
    ReviewService -->|Usa| ReviewRepository

    UserRepository -->|Accede a la tabla de usuarios en| Database
    ReviewRepository -->|Accede a la tabla de reseñas en| Database

    %% Estilos de C4
    classDef external fill:#999999,stroke:#666666,color:#FFFFFF;
    classDef component fill:#85BBF0,stroke:#5D82A8,color:#222222;
    classDef repository fill:#438DD5,stroke:#2E6295,color:#FFFFFF;

    class WebApp,Database,ExtAPI external;
    class AuthController,GameController,ReviewController,AuthService,GameService,ReviewService component;
    class UserRepository,ReviewRepository repository;
```
