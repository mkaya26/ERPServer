using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Users.PasswordChangeUser
{
    public sealed record PasswordChangeUserCommand(
        Guid Id,
        string oldPassword,
        string newPassword) : IRequest<Result<string>>;
}
