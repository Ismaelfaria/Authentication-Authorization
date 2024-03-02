using ApiToken.Model;
using ApiToken.Persistence;
using ApiToken.Repositories.Interfaces;

namespace ApiToken.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextLogin _context;

        public UserRepository(ContextLogin context)
        {
            _context = context;
        }

        public void Add(Login user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public Login GetByUserName(string userName)
        {
            var user = _context.User.FirstOrDefault(de => de.UserName == userName);

            if(user == null)
            {
                return null;
            }

            return user;
        }
    }
}
