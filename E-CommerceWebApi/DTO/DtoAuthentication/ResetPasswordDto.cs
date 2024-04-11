using System.ComponentModel.DataAnnotations;

namespace E_CommerceWebApi.DTO.DtoAuthentication
{
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
