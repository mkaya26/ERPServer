using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.RequirementsPlanningByOrderId
{
    public sealed record RequirementsPlanningByOrderIdCommand(
        Guid Id) : IRequest<Result<RequirementsPlanningByOrderIdCommandResponse>>;
}
