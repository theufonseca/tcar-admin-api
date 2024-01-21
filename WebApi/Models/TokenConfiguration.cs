namespace WebApi.Models
{
    public class TokenConfiguration
    {
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public int Seconds { get; set; }
        public string SecretJwtKey { get; set; } = default!;
    }
}
