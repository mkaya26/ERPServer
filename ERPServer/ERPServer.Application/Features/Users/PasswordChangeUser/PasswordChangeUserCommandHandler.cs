using ERPServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace ERPServer.Application.Features.Users.PasswordChangeUser
{
    internal sealed class PasswordChangeUserCommandHandler(
        UserManager<AppUser> userManager) : IRequestHandler<PasswordChangeUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(PasswordChangeUserCommand request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.FindByIdAsync(request.Id.ToString());
            //
            if(appUser is null)
            {
                return Result<string>.Failure("Kullanıcı Bulunamadı.");
            }
            //
            IdentityResult result = await userManager.ChangePasswordAsync(appUser, request.oldPassword, request.newPassword);
            //
            if (!result.Succeeded)
            {
                return Result<string>.Failure(string.Join(", ", result.Errors.Select(s=> s.Description)));
            }
            //
            return "Parola Başarıyla Değiştirildi.";
        }
    }
}
