using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Users.GetUserById
{
    internal sealed class GetUserByIdQueryHasnler(
        IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, Result<AppUser>>
    {
        public async Task<Result<AppUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await userRepository.GetByExpressionAsync(f => f.Id == request.Id);
            return user;
        }
    }
}
