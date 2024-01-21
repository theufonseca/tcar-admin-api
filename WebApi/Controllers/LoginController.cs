using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenConfiguration tokenConfiguration;
        private readonly IConfiguration configuration;

        public LoginController(TokenConfiguration tokenConfiguration, IConfiguration configuration)
        {
            this.tokenConfiguration = tokenConfiguration;
            this.configuration = configuration;
        }

        [HttpGet("{secretKey}")]
        public IActionResult Get(string secretKey)
        {
            if (secretKey is null) throw new ArgumentException("Authentication Error");

            byte[] bytes = Encoding.UTF8.GetBytes(secretKey);
            var base64Secret = Convert.ToBase64String(bytes);

            var currentSecret = configuration.GetSection("SecretAdmin").Value!.ToString();

            if (currentSecret == base64Secret)
            {
                var token = GetToken(new List<Claim>());
                return Ok(token);
            }

            throw new ArgumentException("Invalid Secret");
        }

        private TokenModel GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.SecretJwtKey));

            var token = new JwtSecurityToken(
                issuer: tokenConfiguration.Issuer,
                audience: tokenConfiguration.Audience,
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
