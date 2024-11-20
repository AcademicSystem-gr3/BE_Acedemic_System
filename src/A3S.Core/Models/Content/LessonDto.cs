using A3S.Core.Domain.Entities;
using A3S.Core.Domain.Identity;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Models.Content
{
    public class LessonDto
    {
        public required Guid LessionId { get; set; }
        public string LessionName { get; set; }
        public bool Status { get; set; }
        public required Guid ClassId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Lesson, LessonDto>();
            }
        }
    }
}
