    using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialClint._models;
using SocialClint._services.Classes;
using SocialClint._services.Interfaces;
using SocialClint.Dto;
using SocialClint.entity;
using System.Security.Claims;

namespace SocialClint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthService auth, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager, ImailService mailservice)
        {
            _auth = auth;
            _userManager = userManager;
            _mailservice = mailservice;
        }

        public IAuthService _auth { get; }
        public Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager { get; }
        public ImailService _mailservice { get; }

        [HttpPost("Register")]



        
        public async Task<ActionResult<AuthModel>> SignUp([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authModel = await _auth.Register(model);
            if (!authModel.IsAuthenticated)
            {

                return BadRequest( );
            }
            var user = await _userManager.FindByIdAsync(authModel.Id);
            var Confirmationtoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmUrl = Url.Action("EmailConfirmation", "Auth", new { userId = authModel.Id, token = Confirmationtoken });
            await _mailservice.SentEmailAsync(user.Email, "confirm your email","hi there welcom to social app");
            return Ok(authModel);

        }

        [HttpPost("GetToken")]
        public async Task<ActionResult<AuthModel>> GetToken([FromBody] TokenRequest tokenRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authmodel = await _auth.GetToken(tokenRequest);
            if (!authmodel.IsAuthenticated)
            {

                return BadRequest(authmodel.Message);
            }
            return Ok(authmodel);

        }


        [HttpPost("EmailConfirmation")]
        public async Task<ActionResult> EmailConcrmation(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("user not found");

            }
            var rslt = await _userManager.ConfirmEmailAsync(user, token);
            if (!rslt.Succeeded) {

                return BadRequest();
            }
            return Ok(rslt);
        }


    }
}
