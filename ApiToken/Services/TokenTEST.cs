using ApiToken.Dtos;
using ApiToken.Repositories.Interfaces;
using ApiToken.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiToken.Services
{
    public class TokenTEST : ITokenService
    {
        private readonly IConfiguration _c;
        private readonly IUserRepository _r;

        public TokenTEST(IConfiguration c, IUserRepository r)
        {
            _c = c;
            _r = r;
        }

        public string GenerateToken(LoginDto l)
        {
            var u = _r.GetByUserName(l.UserName);

            if(u.UserName != l.UserName || u.Password != l.Password)
            {
                return string.Empty;
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_c["Jwt:Key"] ?? string.Empty));
            var Issuer = _c["Jwt:Issuer"];
            var Audience = _c["Jwt:Audience"];

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenJWT = new JwtSecurityToken(

                issuer: Issuer,
                audience: Audience,
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, u.UserName),
                    new Claim(ClaimTypes.Role, u.Role),
                },
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(30)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenJWT);

            return token;


        }
    }
}
