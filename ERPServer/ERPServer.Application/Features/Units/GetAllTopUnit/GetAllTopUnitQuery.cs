using MediatR;
using TS.Result;
using Unit = ERPServer.Domain.Entities.Unit;

namespace ERPServer.Application.Features.Units.GetAllTopUnit
{
    public sealed record GetAllTopUnitQuery() : IRequest<Result<List<Unit>>>;
}
