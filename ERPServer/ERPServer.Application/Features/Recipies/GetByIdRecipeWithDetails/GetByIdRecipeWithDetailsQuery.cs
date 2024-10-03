using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Recipies.GetByIdRecipeWithDetails
{
    public sealed record GetByIdRecipeWithDetailsQuery(
        Guid Id) : IRequest<Result<Recipe>>;
}
