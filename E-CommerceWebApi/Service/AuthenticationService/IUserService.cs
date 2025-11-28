using E_CommerceWebApi.DTO.DtoAuthentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace E_CommerceWebApi.Service.AuthenticationService
{
    public interface IUserService
    {
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto dto, ClaimsPrincipal user);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
    }
}
