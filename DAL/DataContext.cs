using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialClint.Entities;
using SocialClint.entity;

namespace SocialClint.DAL
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions opt) : base(opt)
        {

        }
        public DbSet<Photo> photos { get; set; }
        public DbSet<AppUser> users { get; set; }   
        public DbSet<UserLikes> Likes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLikes>()
                .HasKey(u => new { u.LikedUserId, u.SourceUserId });

            builder.Entity<UserLikes>()
                .HasOne(u => u.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(u => u.SourceUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserLikes>()
               .HasOne(u => u.LikedUser)
               .WithMany(l => l.LikedByUser)
               .HasForeignKey(u => u.LikedUserId)
               .OnDelete(DeleteBehavior.NoAction);
        }

    }

}
