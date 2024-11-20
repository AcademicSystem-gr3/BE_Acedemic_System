using A3S.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Models.Content
{
    public class HomeworkDto
    {
        public Guid HomeworkId { get; set; }
        public Guid ClassId { get; set; }
        public Guid CreateBy { get; set; }
        public string fileName { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Homework, HomeworkDto>();
            }
        }
    }
}
