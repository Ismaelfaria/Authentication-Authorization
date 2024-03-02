using ApiToken.Dtos;

namespace ApiToken.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(LoginDto login);
    }
}
