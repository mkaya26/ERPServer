using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Recipies.DeleteRecipeById
{
    public sealed record DeleteRecipeByIdCommand(
        Guid Id) : IRequest<Result<string>>;
}
