/*using ApiToken.Dtos;
using ApiToken.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiToken.Services
{
    public class TokenTEST
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public TokenTEST(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public string GerarToken(LoginDto user)
        {
            var userDatabase = _userRepository.GetByUserName(user.UserName);
            if(userDatabase.UserName != user.UserName || userDatabase.Password != user.Password)
            {
                return string.Empty;
            }

            var secretyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signingCredential = new SigningCredentials(secretyKey, SecurityAlgorithms.HmacSha256);

            var tokenOption = new JwtSecurityToken(
                issuer:issuer,
                audience:audience,
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, userDatabase.UserName),
                    new Claim(ClaimTypes.Role, userDatabase.Role),
                },
                expires:DateTime.Now.AddMinutes(2),
                signingCredentials:signingCredential);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return token;
        }
    }
}*/
