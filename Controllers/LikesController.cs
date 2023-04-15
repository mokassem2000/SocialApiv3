using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialClint.Dto;
using SocialClint.Entities;
using SocialClint.entity;
using SocialClint.Repository.Repo;

namespace SocialClint.Controllers
{
    public class LikesController : ControllerBase
    {
        public LikesController(UserRepo userRepo, LikeRepo likeRepo, IMapper mapper)
        {
            UserRepo = userRepo;
            LikeRepo = likeRepo;
            Mapper = mapper;
        }

        public UserRepo UserRepo { get; }
        public LikeRepo LikeRepo { get; }
        public IMapper Mapper { get; }

        public async Task<IActionResult> AddLikes(string id)
        {
            var appUser = Mapper.Map<AppUser>(await UserRepo.GetByIdAsync(User.FindFirst("uid")?.Value));
            var LikedUser = await UserRepo.GetByIdAsync(id);
            var SourceUSerWithLikes = await LikeRepo.GetUserWithLikes(appUser.Id);
            if (LikedUser == null) return NotFound();
            if (SourceUSerWithLikes.UserName == appUser.UserName) return BadRequest("you cant like youtrself");
            var userLike = await LikeRepo.GetUserLike(appUser.Id, LikedUser.MemberId);
            if (userLike != null) return BadRequest("you alredy liked this user");

            userLike = new UserLikes
            {
                SourceUserId = appUser.Id,
                LikedUserId = LikedUser.MemberId
            };

            appUser.LikedUsers.Add(userLike);
            if (await UserRepo.saveChaengesAsync())
            {
                return BadRequest();
            }
            return Ok();


        }

        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUsersLikes(string p)
        {
            var appUser = Mapper.Map<AppUser>(await UserRepo.GetByIdAsync(User.FindFirst("uid")?.Value));
            return  Ok(await LikeRepo.GetUserLikes(p, appUser.Id));
            

        }

}
}
