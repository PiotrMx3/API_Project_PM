using API_Project_PM.Core.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace API_Project_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            string expectedUsername = _config["AdminCredentials:Username"]!;
            string expectedHash = _config["AdminCredentials:PasswordHash"]!;

            string incomingHash = Convert.ToHexString(
                SHA256.HashData(Encoding.UTF8.GetBytes(dto.Password))
            ).ToLower();

            if (dto.Username != expectedUsername || incomingHash != expectedHash)
                return Unauthorized();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(int.Parse(_config["Jwt:ExpirationHours"]!)),
                signingCredentials: creds
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
