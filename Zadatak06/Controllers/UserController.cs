using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Zadatak06.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static Dictionary<string, string> RefreshTokens = new();

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "12345678")
            {
                var accessToken = GenerateJwtToken(username);
                var refreshToken = GenerateRefreshToken();

                RefreshTokens[username] = refreshToken;

                return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
            }

            return Unauthorized("Invalid username or password.");
        }

        [HttpPost("refreshtoken")]
        public IActionResult RefreshToken(string username, string refreshToken)
        {
            if (RefreshTokens.TryGetValue(username, out var storedToken) && storedToken == refreshToken)
            {
                var newAccessToken = GenerateJwtToken(username);
                var newRefreshToken = GenerateRefreshToken();

                RefreshTokens[username] = newRefreshToken;

                return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
            }

            return Unauthorized("Invalid refresh token or username.");
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
