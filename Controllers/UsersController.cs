using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialClint._services.Classes;
using SocialClint.DAL;
using SocialClint.Dto;
using SocialClint.entity;
using SocialClint.Repository;
using SocialClint.Repository.Interfaces;
using System.Security.Claims;

namespace SocialClint.Controllers
{
    [Route("api/[Controller]")]
    //[ApiController]
    public class UsersController : ControllerBase
    {

        public UsersController(IRepository<MemberDto> repo,
                               IMapper mapper,
                               PhotoService photoService)
        {
            Repo = repo;
            _Mapper = mapper;

            _photoService = photoService;
        }

        public DataContext Context { get; }

        public IRepository<MemberDto> Repo { get; }
        public IMapper _Mapper { get; }
        public UserManager<AppUser> UserManager { get; }
        public PhotoService _photoService { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers([FromQuery]int CurrenPage, int PageSize)
        {

            var users = await Repo.AllAsync();
            var UserCount = users.Count();
            var pager=new Pager<MemberDto>(UserCount,CurrenPage,PageSize<1?1:PageSize);
            pager.Items = users.Skip(((CurrenPage - 1) * pager.PageSize)).Take(pager.PageSize);

            return Ok(pager);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(string Id)
        {

            return Ok(await Repo.GetByIdAsync(Id));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AppUser>> UpdateUser(UpdateUserDto member, string id)
        {

            if (!ModelState.IsValid)
            {
                var user = await Repo.GetByIdAsync(id);
                if (user == null)
                {
                    return BadRequest("there is no user by this id pleas try another id");

                }
                var updateduser = _Mapper.Map(member, user);
                if (!await Repo.UpdateAsync(updateduser))
                {

                    return BadRequest("somthing goes wrong");
                }

                return Ok(updateduser);
            }
            return BadRequest("oops");


        }





        [HttpPost("add-photo")]
        [Authorize]
        public async Task<ActionResult<PhotoDto>> AddPhoto([FromForm] IFormFile file)
        {
            var user = _Mapper.Map<AppUser>(await Repo.GetByIdAsync(User.FindFirst("uid")?.Value));
            if (user == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (user.photos.Count() == 0) photo.IsMain = true;

            user.photos.Add(photo);
            var updatedsuer = await Repo.UpdateAsync(_Mapper.Map<MemberDto>(user));
            if (!updatedsuer)
            {
                return BadRequest("Problem adding photo");
            }
            return Ok(_Mapper.Map<PhotoDto>(photo));
        }


        [HttpGet("setMain/{photoid}")]
        [Authorize]
        public async Task<ActionResult<PhotoDto>> mainPhoto(string photoid)
        {

            var user = _Mapper.Map<AppUser>(await Repo.GetByIdAsync(User.FindFirst("uid")?.Value));
            if (user == null) return NotFound();
            var mainPhoto = user.photos.FirstOrDefault(p => p.IsMain);
            mainPhoto.IsMain = false;
            user.photos.FirstOrDefault(p => p.Id == int.Parse(photoid)).IsMain = true;
            if (!await Repo.UpdateAsync(_Mapper.Map<MemberDto>(user)))
            {
                return BadRequest("somthing goes wrong");
            }
            return Ok(user.photos);
        }
        [HttpGet("DeletePhoto/{photoid}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeletePhoto(string photoid)
        {

            var user = _Mapper.Map<AppUser>(await Repo.GetByIdAsync(User.FindFirst("uid")?.Value));
            if (user == null) return NotFound();
            var photo = user.photos.FirstOrDefault(p => p.Id == int.Parse(photoid));
            if (photo is null) return NotFound();
            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null)
                {
                    return BadRequest($"Error: {result.Error}");

                }
                user.photos.Remove(photo);

            }
            if (photo.IsMain)
            {
                return BadRequest($"cant remove main photo");

            }
            if (await Repo.UpdateAsync(_Mapper.Map<MemberDto>(user)))
            {
                return Ok(true);
            }
            else
            {
                return BadRequest("failed to delete the photo");
            }
        }
    }
}
