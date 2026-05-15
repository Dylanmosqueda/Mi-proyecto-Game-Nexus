namespace Game_Nexus.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Consola { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        // --- NUEVOS CAMPOS: ESPECIFICACIONES TÉCNICAS ---
        public string Desarrollador { get; set; } = string.Empty; // Ej: Unreal, Unity, Slipspace
        public string EstadoProgreso { get; set; } = string.Empty; // Pendiente, Completado, En Desarrollo

        // --- NUEVOS CAMPOS: BIBLIOTECA PERSONAL ---
        public DateTime FechaAdquisicion { get; set; } = DateTime.Now;
        public int HorasDedicadas { get; set; }
        public int Calificacion { get; set; } // Escala 1-10 o 1-100
        public string VinculacionProyecto { get; set; } = string.Empty;
        // Campo para la imagen
        public string? ImagenUrl { get; set; }
    }
}
