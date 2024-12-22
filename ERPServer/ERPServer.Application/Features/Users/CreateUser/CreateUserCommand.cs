using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Users.CreateUser
{
    public sealed record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string UserName,
        string Password) : IRequest<Result<string>>;
}
