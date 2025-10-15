using AutoMapper;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.DTOs;

namespace TheRememberer.Objects
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EntityBase, DtoBase>().ReverseMap()
                .ForMember(dest => dest.Id, opt =>
                {
                    opt.MapFrom(src => src.DbId);
                    opt.Condition(src => src.DbId != null);
                });
            CreateMap<User, UserDto>().IncludeBase<EntityBase, DtoBase>().ReverseMap();
        }
    }
}
