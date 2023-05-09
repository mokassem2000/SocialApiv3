using Microsoft.EntityFrameworkCore;
using SocialClint.DAL;
using SocialClint.Dto;
using SocialClint.Entities;
using SocialClint.entity;
using SocialClint.Repository.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace SocialClint.Repository.Repo
{
    public class LikeRepo : ILikeRepo
    {
        public LikeRepo(DataContext dataContext)
        {
            _context = dataContext;
        }

        public DataContext _context { get; }

        public async Task<UserLikes> GetUserLike(string sourceUserId, string targetUserId)
        {
            return await _context.Likes.FirstOrDefaultAsync(l => l.SourceUserId == sourceUserId && l.LikedUserId == targetUserId);
        }
        public async Task<IEnumerable<LikeDto>> GetUserLikes(string likesParams, string UserID)
        {
            var users = _context.users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if (likesParams == "liked")
            {
                likes = likes.Where(like => like.SourceUserId == UserID);
                users = likes.Select(like => like.LikedUser);
            }

            if (likesParams == "likedBy")
            {
                likes = likes.Where(like => like.LikedUserId == UserID);
                users = likes.Select(like => like.SourceUser);
            }

            return await users.Select(user => new LikeDto()
            {
                memberId = user.Id,
                UserName = user.UserName,
                KnowenAs = user.KnowenAs,
                City = user.City,
                age = (int)((DateTime.Now - user.DateOfBirth).TotalDays)/ 365,
                PhotoUrl = user.photos.FirstOrDefault(p => p.IsMain).Url
            }
            ).ToListAsync();
        }
        public async Task<AppUser> GetUserWithLikes(string userId)
        {
            return await _context.users.Include(u => u.LikedUsers)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }

}