using System.ComponentModel.DataAnnotations;

namespace AZFlow360.API.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public int RoleID { get; set; }
    }

    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string Token { get; set; }
    }
}
