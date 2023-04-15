using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using SocialClint._services.Classes;
using SocialClint.DAL;
using SocialClint.Dto;
using SocialClint.entity;
using SocialClint.Repository.Interfaces;
using System.Drawing.Printing;


namespace SocialClint.Repository.Repo
{
    public class UserRepo : IRepository<MemberDto>
    {
        public UserRepo(DataContext dataContext, IMapper mapper)
        {
            context = dataContext;
            Mapper = mapper;
        }

        public DataContext context { get; }
        public IMapper Mapper { get; }

        public async Task<IEnumerable<MemberDto>> AllAsync()
        {

            var users = context.Users.Include(u => u.photos);
            return Mapper.Map<IEnumerable<MemberDto>>(users);


        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                context.Users.Remove(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<MemberDto> GetByIdAsync(string id)
        {
            var user = await context.Users.AsNoTracking().Include(u => u.photos).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;

            }
            return Mapper.Map<MemberDto>(user);
        }

        public async Task<bool> saveChaengesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }



        public async Task<bool> UpdateAsync(MemberDto entity)
        {
            var appuser = await context.Users.FindAsync(entity.MemberId);
            appuser.Introduction = entity.Introduction;
            appuser.Intersts = entity.Intersts;
            appuser.City = entity.City;
            appuser.Country = entity.Country;
            appuser.LookingFor = entity.LookingFor;
            appuser.photos = Mapper.Map<List<Photo>>(entity.photos);
            context.Users.Update(appuser);
            return await saveChaengesAsync();
        }


    }
}
