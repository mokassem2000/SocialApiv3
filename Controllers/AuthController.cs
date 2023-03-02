using Microsoft.AspNetCore.Mvc;
using SocialClint._models;
using SocialClint._services.Classes;
using SocialClint._services.Interfaces;

namespace SocialClint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        public IAuthService _auth { get; }

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
                return BadRequest(authModel.Message);

            }
            return Ok(authModel);

        }

        [HttpPost("GetToken")]
        public async Task<ActionResult<AuthModel>> GetToken(TokenRequest tokenRequest) 
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            var authmodel= await _auth.GetToken(tokenRequest);
            if (!authmodel.IsAuthenticated)
            {

                return BadRequest(authmodel.Message);
            }
            return Ok(authmodel);
        
        }
    }
}
