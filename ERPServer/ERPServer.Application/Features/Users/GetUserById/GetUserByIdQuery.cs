using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Users.GetUserById
{
    public sealed record GetUserByIdQuery(
        Guid Id) : IRequest<Result<AppUser>>;
}
