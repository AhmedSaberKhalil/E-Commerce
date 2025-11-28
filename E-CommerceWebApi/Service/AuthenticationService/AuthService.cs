using E_CommerceWebApi.Data;
using E_CommerceWebApi.DTO.DtoAuthentication;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceWebApi.Service.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }
        public async Task<string> Login(UserLoginDto dto)
        {
            ApplicationUser user = await userManager.FindByNameAsync(dto.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, dto.Password))
            {
                var roles = await userManager.GetRolesAsync(user);
                return tokenService.GenerateToken(user, roles);
            }

            return null;

        }

        public async Task<IdentityResult> Register(UserRegisterationDto dto)
        {

            var user = new ApplicationUser { UserName = dto.Username, Email = dto.Email };

            IdentityResult result = await userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false); // flase =>  session coockie عشان لو نسي الباسورد يرجع يسجل تاني
                return result;
            }
            else
                return (IdentityResult)result.Errors;
        }
    }
}
