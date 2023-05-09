using Microsoft.AspNetCore.Identity;
using SocialClint.Entities;

namespace SocialClint.entity
{
    public class AppUser : IdentityUser
    {
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnowenAs { get; set; }
        public string Introduction { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string LookingFor { get; set; }
        public string Intersts { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Photo> photos { get; set; }

        public List<UserLikes> LikedUsers { set; get; }

        public List<UserLikes> LikedByUser { set; get; }

        public List<Message> messageSent { set; get; }

        public List<Message> messageRecived { set; get; }


    }
}
