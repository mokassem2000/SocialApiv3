using AutoMapper;
using SocialClint.entity;

namespace SocialClint.Dto.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, MemberDto>().ForMember(dest => dest.MemberId, src => src.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<UpdateUserDto, MemberDto>();
            CreateMap<Photo,PhotoDto>().ReverseMap();

        }

    }
}
