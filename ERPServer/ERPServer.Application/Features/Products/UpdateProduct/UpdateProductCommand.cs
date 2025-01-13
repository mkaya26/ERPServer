using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.UpdateProduct
{
    public sealed record UpdateProductCommand(
        Guid Id,
        string Name,
        int TypeValue,
        Guid UnitId) : IRequest<Result<string>>;
}
