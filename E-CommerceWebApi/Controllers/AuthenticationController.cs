using E_CommerceWebApi.DTO.DtoAuthentication;
using E_CommerceWebApi.Service.AuthenticationService;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthenticationController(IAuthService authService, IUserService userService)
        {
            this._authService = authService;
            this._userService = userService;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterationDto registerationDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Register(registerationDto);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        return BadRequest(item.Description);
                    }
                }


            }

            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var token = await _authService.Login(loginDto);
                if (token == null)
                {
                    return Unauthorized(new { message = "Invalid username or password." });
                }
                return Ok(token);
            }
            return Unauthorized();

        }
        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangePasswordAsync(changePasswordDto, User);

                if (result.Succeeded)
                {
                    return Ok(new { message = "Password changed successfully." });
                }

                return BadRequest(new
                {
                    message = "Password change failed.",
                    errors = result.Errors.Select(e => e.Description)
                });
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost("Forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var result = await _userService.ForgotPasswordAsync(dto);
            return Ok(result);
        }

        [HttpPost("Reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _userService.ResetPasswordAsync(dto);
            return result ? Ok("Password reset") : BadRequest("Failed to reset password");
        }
    }
}
