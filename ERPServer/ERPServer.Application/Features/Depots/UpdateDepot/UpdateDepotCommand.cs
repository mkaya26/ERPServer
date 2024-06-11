using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.UpdateDepot
{
    public record class UpdateDepotCommand(
        Guid Id,
        string Name,
        string City,
        string Town,
        string FullAddress) : IRequest<Result<string>>;
}
