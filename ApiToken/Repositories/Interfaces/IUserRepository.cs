using ApiToken.Model;

namespace ApiToken.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Add(Login user);
        Login GetByUserName(string userName);
    }
}
