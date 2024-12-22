using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories
{
    internal sealed class UnitRepository : Repository<Unit, ApplicationDbContext>, IUnitRepository
    {
        public UnitRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
