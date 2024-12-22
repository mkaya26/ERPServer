using ERPServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Users.GetAllUsers
{
    internal sealed class GetAllUsersQueryHandler(
    UserManager<AppUser> userManager) : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersQueryResponse>>>
    {
        public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<AppUser> users = await userManager.Users.OrderBy(p => p.FirstName).ToListAsync(cancellationToken);
            //
            List<GetAllUsersQueryResponse> response =
                users.Select(s => new GetAllUsersQueryResponse()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    UserName = s.UserName,
                    FullName = s.FullName
                }).ToList();
            //
            return response;
        }
    }
}
