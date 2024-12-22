using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Users.GetAllUsers
{
    public sealed record GetAllUsersQuery() : IRequest<Result<List<GetAllUsersQueryResponse>>>;
}
