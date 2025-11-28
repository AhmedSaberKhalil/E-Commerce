using E_CommerceWebApi.DTO.DtoAuthentication;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceWebApi.Service.AuthenticationService
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(UserRegisterationDto dto);
        Task<string> Login(UserLoginDto dto);
    }
}
