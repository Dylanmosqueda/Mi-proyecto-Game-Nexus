namespace Game_Nexus.API
{
    public class TokenService
    {
        public bool ValidateKeyLength(string key)
        {
            // Las firmas simétricas HMACSHA256 requieren claves de al menos 256 bits (32 caracteres)
            return !string.IsNullOrEmpty(key) && key.Length >= 32;
        }
    }
}
