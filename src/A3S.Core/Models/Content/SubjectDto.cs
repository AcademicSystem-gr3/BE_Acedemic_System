using A3S.Core.Domain.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using A3S.Core.Domain.Entities;
using AutoMapper;

namespace A3S.Core.Models.Content
{
    public class SubjectDto
    {
        public required Guid SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public Guid CreatorBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? TeacherID { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Subject, SubjectDto>();
            }
        }
    }
}
