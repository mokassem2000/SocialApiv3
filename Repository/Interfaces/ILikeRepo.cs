using SocialClint._services.Classes;
using SocialClint.Dto;
using SocialClint.Entities;
using SocialClint.entity;

namespace SocialClint.Repository.Interfaces
{
    public interface ILikeRepo
    {

   
       Task<UserLikes> GetUserLike(string sourceUserId, string targetUserId);

        Task<IEnumerable<LikeDto>> GetUserLikes(string likesParams, string UserID);

         Task<AppUser> GetUserWithLikes(string userId);

    }
}
