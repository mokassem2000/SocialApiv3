using AutoMapper;
using SocialClint.Entities;
using SocialClint.entity;

namespace SocialClint.Dto.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, MemberDto>().ForMember(dest => dest.MemberId, src => src.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<UpdateUserDto, MemberDto>();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Message, MessageDto>()
                .ForMember(des => des.SenderPhotoUrl, opt => opt.MapFrom(src => src.Sender.photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(des => des.RecipientPhotoUrl, opt => opt.MapFrom(src => src.Recipient.photos.FirstOrDefault(x => x.IsMain).Url));

        }

    }
}
