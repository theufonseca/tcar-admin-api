namespace WebApi.Models
{
    public class TokenModel
    {
        public string Token { get; set; } = default!;
        public DateTime ValidTo { get; set; }
    }
}
