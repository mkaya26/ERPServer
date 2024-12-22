using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Users.UpdateUser
{
    public sealed record UpdateUserCommand(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string UserName) : IRequest<Result<string>>;
}
