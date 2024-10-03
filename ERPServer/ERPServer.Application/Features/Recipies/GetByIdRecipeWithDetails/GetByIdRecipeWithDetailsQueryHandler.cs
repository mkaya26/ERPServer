using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Recipies.GetByIdRecipeWithDetails
{
    internal sealed class GetByIdRecipeWithDetailsQueryHandler(
        IRecipeRepository recipeRepository) :
        IRequestHandler<GetByIdRecipeWithDetailsQuery, Result<Recipe>>
    {
        public async Task<Result<Recipe>> Handle(GetByIdRecipeWithDetailsQuery request, CancellationToken cancellationToken)
        {
            Recipe? recipe = await recipeRepository
                .Where(f => f.Id == request.Id)
                .Include(f => f.Product)
                .Include(f => f.RecipeDetails!)
                .ThenInclude(f => f.Product)
                .FirstOrDefaultAsync(cancellationToken);
            //
            if(recipe is null)
            {
                return Result<Recipe>.Failure("Reçete Bulunamadı.");
            }
            //
            return recipe;
        }
    }
}
