using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Units.CreateSystemUnit
{
    public sealed class CreateSystemUnitCommand() : IRequest<Result<string>>;
}
