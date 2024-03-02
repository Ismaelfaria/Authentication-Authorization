using ApiToken.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiToken.Persistence
{
    public class ContextLogin : DbContext
    {
        public ContextLogin(DbContextOptions<ContextLogin> options) : base(options)
        {}

        public DbSet<Login> User { get; set; }
    }
}
