using System;
using System.ComponentModel.DataAnnotations; // <--- ESTA ES LA LÍNEA QUE FALTA

namespace Game_Nexus.Models
{
    public enum EstadoProgreso { Pendiente, EnDesarrollo, Completado, Abandonado }

    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(100, MinimumLength = 2)]
        public string Titulo { get; set; } = string.Empty;

        public string Genero { get; set; } = string.Empty;

        [Range(1950, 2100, ErrorMessage = "Año fuera de rango")]
        public int Ano { get; set; }

        public string Consola { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Desarrollador { get; set; } = string.Empty;

        public EstadoProgreso Estado { get; set; }

        public DateTime FechaAdquisicion { get; set; } = DateTime.Now;

        [Range(0, 5000, ErrorMessage = "Las horas deben ser positivas")]
        public int HorasDedicadas { get; set; }

        [Range(1, 10, ErrorMessage = "La calificación debe ser entre 1 y 10")]
        public int Calificacion { get; set; }

        public string VinculacionProyecto { get; set; } = string.Empty;

        public string? ImagenUrl { get; set; }
    }
}