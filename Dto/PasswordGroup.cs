using System.ComponentModel.DataAnnotations;

namespace SocialClint.Dto
{
    public class PasswordGroup
    {
        [Required, StringLength(256)]
        public string Password { get; set; }
        [Required, StringLength(256)]
        public string ConfirmPassword { get; set; }
    }
}
