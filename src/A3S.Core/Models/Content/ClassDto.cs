using A3S.Core.Domain.Entities;
using A3S.Core.Domain.Identity;
using AutoMapper;

namespace A3S.Core.Models.Content
{
    public class ClassDto
    {
        public required Guid ClassId { get; set; }
        public required string Name { get; set; }
        public Guid CreatorId { get; set; }

        public string ClassCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ImageThemes { get; set; }
        public UserDto User { get; set; }
        public BlockDto Block { get; set; }
        public Guid BlockId { get; set; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Class, ClassDto>().ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Creator))
                    .ForMember(dest=>dest.Block,opt=>opt.MapFrom(src=>src.Block)); 
                CreateMap<User, UserDto>();
                CreateMap<Block, BlockDto>();
            }
        }
    }
}
