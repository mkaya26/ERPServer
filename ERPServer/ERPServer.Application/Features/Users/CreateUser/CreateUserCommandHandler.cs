using AutoMapper;
using ERPServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Users.CreateUser
{
    internal sealed class CreateUserCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper) : IRequestHandler<CreateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.Users.AnyAsync(f => f.Email == request.Email))
            {
                return Result<string>.Failure("Bu email adresi başka bir kullanıcı için eklenmiştir.");
            }
            //
            if (await userManager.Users.AnyAsync(u => u.UserName == request.UserName))
            {
                return Result<string>.Failure("Bu kullanıcı adı başka bir kullanıcı için eklenmiştir.");
            }
            //
            AppUser user = mapper.Map<AppUser>(request);
            IdentityResult result = await userManager.CreateAsync(user, request.Password);
            //
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }
            //
            return "Kullanıcı Başarıyla Eklendi.";
        }
    }
}
