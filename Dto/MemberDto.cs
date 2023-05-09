using SocialClint.Entities;
using SocialClint.entity;

namespace SocialClint.Dto
{
    public class MemberDto
    {
        public string MemberId { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnowenAs { get; set; }
        public string Introduction { get; set; }
        public DateTime Created { get; set; } 
        public DateTime LastActive { get; set; } 
        public string Gender { get; set; }
        public string LookingFor { get; set; }
        public string Intersts { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<PhotoDto> photos { get; set; }
        public List<UserLikes> LikedUsers { set; get; }

        public List<UserLikes> LikedByUser { set; get; }
    }
}
