namespace SDKolej.API.Models
{
    public class JwtSettings
    {
        public string? SecretKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpirationInMinutes { get; set; } = 60;
        public int RefreshTokenExpirationInDays { get; set; } = 7;
    }
} 