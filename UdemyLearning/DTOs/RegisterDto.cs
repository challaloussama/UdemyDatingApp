using System.ComponentModel.DataAnnotations;

namespace UdemyLearning.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
