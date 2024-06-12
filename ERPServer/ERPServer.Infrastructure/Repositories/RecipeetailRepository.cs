using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories
{
    internal sealed class RecipeetailRepository : Repository<RecipeDetail, ApplicationDbContext>, IRecipeDetailRepository
    {
        public RecipeetailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
