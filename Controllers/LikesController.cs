using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialClint.Dto;
using SocialClint.Entities;
using SocialClint.entity;
using SocialClint.Repository.Interfaces;
using SocialClint.Repository.Repo;

namespace SocialClint.Controllers
{

    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        public LikesController(IRepository<MemberDto>  userRepo, ILikeRepo likeRepo, IMapper mapper)
        {
            UserRepo = userRepo;
            LikeRepo = likeRepo;
            Mapper = mapper;
        }

        public IRepository<MemberDto> UserRepo { get; }
   
        public ILikeRepo LikeRepo { get; }
        public IMapper Mapper { get; }

        [HttpGet("{id}")]
        [Authorize]
        //var user = _Mapper.Map<AppUser>(await Repo.GetByIdAsync(User.FindFirst("uid")?.Value));
        public async Task<IActionResult> AddLikes(string id)
        {

            var cuserid = User.FindFirst("uid")?.Value;
            var MemberDto= await UserRepo.GetByIdAsync(cuserid);
            

            var appUser = Mapper.Map<AppUser>(MemberDto);




            var LikedUser = await UserRepo.GetByIdAsync(id);



            var SourceUSerWithLikes = await LikeRepo.GetUserWithLikes(appUser.Id);


            if (LikedUser == null) return NotFound();

            if (SourceUSerWithLikes.UserName == LikedUser.UserName) return BadRequest("you cant like youtrself");
            UserLikes userLike = await LikeRepo.GetUserLike(appUser.Id, LikedUser.MemberId);
            if (userLike != null) return BadRequest("you alredy liked this user");



            userLike = new UserLikes()
            {
                SourceUserId = appUser.Id,
                LikedUserId = LikedUser.MemberId
            };

            MemberDto.LikedUsers.Add(userLike);

            if (!await UserRepo.UpdateAsync(MemberDto))
            {
                return BadRequest();
            }

            return Ok();


        }   

        [HttpGet("GetLikes/{p}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUsersLikes([FromRoute]string p)
        {
            var appUser = Mapper.Map<AppUser>(await UserRepo.GetByIdAsync(User.FindFirst("uid")?.Value));
            return  Ok(await LikeRepo.GetUserLikes(p, appUser.Id));
            

        }

}
}
