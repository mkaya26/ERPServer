using AutoMapper;
using ERPServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Users.UpdateUser
{
    internal sealed class UpdateUserCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper) :
    IRequestHandler<UpdateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await userManager.FindByIdAsync(request.Id.ToString());
            //
            if (user is null)
            {
                return Result<string>.Failure("Kullanıcı Bulunamadı.");
            }
            //
            if (user.Email != request.Email)
            {
                if (await userManager.Users.AnyAsync(f => f.Email == request.Email))
                {
                    return Result<string>.Failure("Bu email adresi başka bir kullanıcı için eklenmiştir.");
                }
            }
            //
            if (user.UserName != request.UserName)
            {
                if (await userManager.Users.AnyAsync(u => u.UserName == request.UserName))
                {
                    return Result<string>.Failure("Bu kullanıcı adı başka bir kullanıcı için eklenmiştir.");
                }
            }
            //
            mapper.Map(request, user);
            //
            IdentityResult result = await userManager.UpdateAsync(user);
            //
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }
            //
            return "Kullanıcı Başarıyla Güncellendi.";
        }
    }
}
