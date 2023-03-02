using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialClint.DAL;
using SocialClint.entity;

namespace SocialClint.Controllers
{
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {
        public UsersController(DataContext _context)
        {
            Context = _context;
        }

        public DataContext Context { get; }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
          return Ok(await Context.users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int Id)
        {
            var user=Context.users.Find(Id);
            return Ok(user);
        }
    }
}
