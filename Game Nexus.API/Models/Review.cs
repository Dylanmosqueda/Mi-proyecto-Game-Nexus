namespace Game_Nexus.API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Autor { get; set; } = string.Empty;
        public int Calificacion { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}
