using AutoMapper;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.DTOs;

namespace TheRememberer.Objects
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Base class mappings
            CreateMap<EntityBase, DtoBase>()
                .ForMember(dest => dest.DbId, opt => opt.MapFrom(src => src.Id));

            CreateMap<DtoBase, EntityBase>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DbId));

            // User ↔ UserDto
            CreateMap<User, UserDto>()
                .IncludeBase<EntityBase, DtoBase>()
                .ForMember(dest => dest.DiscordData, opt => opt.MapFrom(src => src.DiscordData))
                .ForMember(dest => dest.UploadedImages, opt => opt.MapFrom(src => src.UploadedImages));

            CreateMap<UserDto, User>()
                .IncludeBase<DtoBase, EntityBase>()
                .ForMember(dest => dest.DiscordData, opt => opt.MapFrom(src => src.DiscordData))
                .ForMember(dest => dest.UploadedImages, opt => opt.MapFrom(src => src.UploadedImages));

            // User_Discord ↔ User_DiscordDto
            CreateMap<User_Discord, User_DiscordDto>().IncludeBase<EntityBase, DtoBase>();

            CreateMap<User_DiscordDto, User_Discord>().IncludeBase<DtoBase, EntityBase>().ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}