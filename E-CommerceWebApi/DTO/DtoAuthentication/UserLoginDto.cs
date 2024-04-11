using System.ComponentModel.DataAnnotations;

namespace E_CommerceWebApi.DTO.DtoAuthentication
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
