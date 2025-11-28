using System.Net;
using System.Security.Claims;
using E_CommerceWebApi.Data;
using E_CommerceWebApi.DTO.DtoAuthentication;
using E_CommerceWebApi.Repository;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceWebApi.Service.AuthenticationService
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;


        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto dto, ClaimsPrincipal user)
        {
            var currentUser = await userManager.GetUserAsync(user);
            if (currentUser == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "The current user could not be found."
                });
            }

            var result = await userManager.ChangePasswordAsync(currentUser, dto.OldPassword, dto.NewPassword);
            if (result.Succeeded)
            {
                await signInManager.RefreshSignInAsync(currentUser);
                return result;
            }
            else {
                return (IdentityResult)result.Errors;
            }

        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"{_configuration["AppBaseUrl"]}/reset-password?token={WebUtility.UrlEncode(token)}";
            var body = $"Click to reset password: {resetLink}";

            await _emailService.SendEmailAsync(dto.Email, "Reset Password", body);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, dto.NewPassword);

            return result.Succeeded;
        }
    }
}
