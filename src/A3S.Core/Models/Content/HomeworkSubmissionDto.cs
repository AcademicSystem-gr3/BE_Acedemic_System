using A3S.Core.Domain.Entities;
using A3S.Core.Domain.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Models.Content
{
    public class HomeworkSubmissionDto
    {
        public required Guid SubmissionId { get; set; }
        public required Guid HomeworkId { get; set; }
        public required Guid UserId { get; set; }
        public StatusHomework status { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string? FeedBack { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<HomeworkSubmission, HomeworkSubmissionDto>();
            }
        }
    }
}
