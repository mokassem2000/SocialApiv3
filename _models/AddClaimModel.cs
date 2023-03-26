using System.ComponentModel.DataAnnotations;

namespace SocialClint._models
{
    public class AddClaimModel
    {
            [Required]
            public string UserId { get; set; }

            [Required]
            public string claimName { get; set; }
    }
}
