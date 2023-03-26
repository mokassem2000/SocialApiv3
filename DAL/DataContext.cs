using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialClint.entity;

namespace SocialClint.DAL
{
    public class DataContext:IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions opt):base(opt)
        {
            
        }
                public DbSet<Photo> photos { get; set; }
      
    }

}
