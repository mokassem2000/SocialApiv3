using System.ComponentModel.DataAnnotations;

namespace SocialClint._models
{
    public class TokenRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
