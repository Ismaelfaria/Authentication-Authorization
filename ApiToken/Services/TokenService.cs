
using ApiToken.Dtos;
using ApiToken.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiToken.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _conf;
        private readonly IUserSevice _get;

        public TokenService(IConfiguration c, IUserSevice s)
        {
            _conf = c;
            _get = s;
        }

        public string GenerateToken(LoginDto login)
        {
            var userData = _get.GetByUserName(login.UserName);

            if (userData == null)
            {
                return string.Empty;
            }

            var secretyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Jwt:Ket"]));
            var issuer = _conf["Jwt:Issuer"];
            var audience = _conf["Jwt:Audience"];

            var signingCredentials = new SigningCredentials(secretyKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(

                issuer: issuer,
                audience: audience,
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, userData.UserName),
                    new Claim(ClaimTypes.Role, userData.Role),
                },
                expires:DateTime.Now.AddHours(2),
                signingCredentials:signingCredentials);


            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }
    }
}
