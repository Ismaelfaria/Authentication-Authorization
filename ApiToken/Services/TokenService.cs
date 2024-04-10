
using ApiToken.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiToken.Dtos;
using ApiToken.Services.Interfaces;

namespace ApiToken.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _config;
        private readonly IUserSevice _user;

        public TokenService(IConfiguration config, IUserSevice user)
        {
            _config = config;
            _user = user;
        }

        public string GenerateToken(LoginDto login)
        {
            var userDatabase = _user.GetByUserName(login.UserName);

            if (login == null)
            {
                return string.Empty;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var issuer = _config["JWT:Issuer"];
            var audience = _config["JWT:Audience"];

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDatabase.UserName),
                    new Claim(ClaimTypes.Role, userDatabase.Role),
                },
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signingCredentials
              );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }        
    }
}
