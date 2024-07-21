

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
        private readonly IConfiguration conf;
        private readonly IUserSevice user;

        public string GenerateToken(LoginDto login)
        {
            var userCheck = user.GetByUserName(login.UserName);
            if (userCheck.UserName != login.UserName || userCheck.Password != login.Password)
            {
                return string.Empty;
            }


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["Jwt:Key"]));
            var issuer = conf["Jwt:Issuer"];
            var audience = conf["Jwt:Audience"];

            var singCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new[]
                {
                    new Claim(ClaimTypes.Role, userCheck.Role),
                    new Claim(ClaimTypes.Name, userCheck.UserName)
                },
                expires: DateTime.Now.AddHours(2),
                signingCredentials: singCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }
    }
}
