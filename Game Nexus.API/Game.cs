namespace Game_Nexus.API
{
    public class Game
    {
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Title)) return false;
            if (ReleaseYear < 1950 || ReleaseYear > DateTime.UtcNow.Year + 2) return false;
            return true;
        }
    }
}
