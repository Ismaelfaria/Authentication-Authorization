
using ApiToken.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiToken.Dtos;

namespace ApiToken.Services.Interfaces
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _respositoryUser;

        public TokenService(IConfiguration configuration, IUserRepository respositoryByUserName)
        {
            _configuration = configuration;
            _respositoryUser = respositoryByUserName;
        }

        public string GenerateToken(LoginDto login)
        {
            var userDatabase = _respositoryUser.GetByUserName(login.UserName);

            if (login.UserName != userDatabase.UserName || login.Password != userDatabase.Password)
            {
                return string.Empty;
            }

            var secretKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var issuer = _configuration["Jwt:issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, userDatabase.UserName),
                    new Claim(ClaimTypes.Role, userDatabase.Role),
                },
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signinCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }
    }
}
