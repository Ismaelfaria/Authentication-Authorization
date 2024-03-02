using ApiToken.Model;
using ApiToken.Repositories.Interfaces;
using ApiToken.Services.Interfaces;

namespace ApiToken.Services
{
    public class UserService : IUserSevice
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(Login user)
        {
            user.Id = Guid.NewGuid();
            _userRepository.Add(user);
        }

        public Login GetByUserName(string userName)
        {
           return _userRepository.GetByUserName(userName);
        }
    }
}
