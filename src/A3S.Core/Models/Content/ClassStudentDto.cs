using A3S.Core.Domain.Entities;
using A3S.Core.Domain.Identity;
using AutoMapper;

namespace A3S.Core.Models.Content
{
    public class ClassStudentDto
    {
        public required Guid ClassId { get; set; }
        public required string Name { get; set; }
        public Guid CreatorId { get; set; }

        public string ClassCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ImageThemes { get; set; }
        public Guid BlockId { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Class, ClassStudentDto>();
            }
        }
    }
}
