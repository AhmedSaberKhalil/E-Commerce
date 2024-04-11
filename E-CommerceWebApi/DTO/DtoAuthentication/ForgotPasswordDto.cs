using System.ComponentModel.DataAnnotations;

namespace E_CommerceWebApi.DTO.DtoAuthentication
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
