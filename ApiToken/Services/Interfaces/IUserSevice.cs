using ApiToken.Model;

namespace ApiToken.Services.Interfaces
{
    public interface IUserSevice
    {
        void Add(Login user);
        Login GetByUserName(string userName);
    }
}
