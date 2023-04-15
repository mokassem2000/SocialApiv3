using SocialClint.entity;

namespace SocialClint.Entities
{
    public class UserLikes
    {
        public AppUser SourceUser { set; get; }
        public string SourceUserId { set;get; }
        public AppUser LikedUser { set;get; }
        public string LikedUserId { set; get; }
    }
}
