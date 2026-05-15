using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using API_Project_PM.Core.DTOs.Auth;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_Minimal_Project_PM.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var loginGroup = app.MapGroup("/api/login").WithTags("Login");

            loginGroup.MapPost("/", Results<Ok<object>, UnauthorizedHttpResult> (LoginDto dto, IConfiguration config) =>
            {
                string expectedUsername = config["AdminCredentials:Username"]!;
                string expectedHash = config["AdminCredentials:PasswordHash"]!;

                string incomingHash = Convert.ToHexString(
                    SHA256.HashData(Encoding.UTF8.GetBytes(dto.Password))
                ).ToLower();

                if (dto.Username != expectedUsername || incomingHash != expectedHash)
                    return TypedResults.Unauthorized();

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Secret"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    expires: DateTime.UtcNow.AddHours(int.Parse(config["Jwt:ExpirationHours"]!)),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return TypedResults.Ok<object>( new { token = tokenString });
            });
        }
    }
}
