namespace Game_Nexus.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string MotorGrafico { get; set; } = string.Empty; // Nuevo
        public string Genero { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Plataforma { get; set; } = string.Empty; // Reemplaza a 'Consola'
        public string EstadoProgreso { get; set; } = string.Empty; // Pendiente, Completado, En Desarrollo
        public string Descripcion { get; set; } = string.Empty;

        // Campo para la imagen
        public string? ImagenUrl { get; set; }
    }

    // ==========================================
    // PILAR 2: USUARIO
    // ==========================================
    public class Usuario
    {
        public int Id { get; set; }
        public string Perfil { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; // Jugador o Desarrollador
        public string Estadisticas { get; set; } = string.Empty;
    }

    // ==========================================
    // PILAR 3: ASSET (Recurso Técnico)
    // ==========================================
    public class Asset
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ArchivoUrl { get; set; } = string.Empty; // Link o binario
        public string Tipo { get; set; } = string.Empty; // Modelo 3D, Audio, Script, Textura
        public string Version { get; set; } = string.Empty;
        public string Etiquetas { get; set; } = string.Empty;
    }

    // ==========================================
    // PILAR 4: PROYECTO / MOD
    // ==========================================
    public class ProyectoMod
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Changelog { get; set; } = string.Empty; // Registro de cambios

        // Relaciones simples (Guardamos solo los IDs)
        public int ItemId { get; set; } // ID del Videojuego (Item)
        public int DesarrolladorId { get; set; } // ID del Usuario
    }

    // ==========================================
    // PILAR 5: BIBLIOTECA PERSONAL
    // ==========================================
    public class BibliotecaPersonal
    {
        public int Id { get; set; }
        public string FechaAdquisicion { get; set; } = string.Empty;
        public decimal HorasDedicadas { get; set; }
        public int Calificacion { get; set; }

        // Relaciones simples
        public int UsuarioId { get; set; }
        public int ItemId { get; set; } // ID del Videojuego (Item) adquirido
        public int ProyectoVinculadoId { get; set; }
    }
}
