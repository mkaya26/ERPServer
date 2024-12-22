using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<AppUser, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
