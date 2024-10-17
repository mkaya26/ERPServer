using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.RecipeDetails.DeleteReceiptDetail
{
    public sealed record DeleteReceiptDetailCommand(
        Guid Id) : IRequest<Result<string>>;
}
