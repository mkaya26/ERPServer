using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Recipies.GetAllRecipe
{
    public sealed record GetAllRecipeQuery() : IRequest<Result<List<Recipe>>>;
}
