using A3S.Core.Domain.Entities;
using A3S.Core.Domain.Identity;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Models.Content
{
    public class QuizDto
    {
        public required Guid QuizId { get; set; }
        public string QuizName { get; set; }
        public float Duration { get; set; }
        public float? PassRate { get; set; }
        public required Guid LessonId { get; set; }
        public bool Status { get; set; }
        public required Guid CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Quiz, QuizDto>();
            }
        }
    }
}
