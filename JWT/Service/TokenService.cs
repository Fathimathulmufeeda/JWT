//Generates JWT and refresh tokens
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
namespace JWT.Service
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(
            int userId,
            string email,
            string role)
        {
            var secretKey = _configuration["Jwt:Key"];

            //the: between Jwt and Key
            //It means:
            //-Go to the Jwt section.
            //-Read its Key property.

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var expiryMinutes = Convert.ToDouble(
                _configuration["Jwt:ExpiryMinutes"]
            );

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey!)
            );


            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };
            var credentials = new SigningCredentials(// SigningCredentials- combines the key and algorithm to specify how the JWT should be signed.
                key,//the secret key retrieved from appsettings.json.
                SecurityAlgorithms.HmacSha256);//HmacSha256 – the cryptographic signing algorithm.
            var token = new JwtSecurityToken(
               issuer: issuer,
               audience: audience,
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
               signingCredentials: credentials
);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
