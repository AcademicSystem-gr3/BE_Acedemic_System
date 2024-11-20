using A3S.Core.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Models.Content
{
    public class BlogDto
    {
        public required Guid BlogId { get; set; }
        public required Guid CreatorId { get; set; }
        public long View { get; set; }
        public long Status { get; set; }
        public string Content { get; set; }
        public int NumLike { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Blog, BlogDto>();
            }
        }
    }
    public class UpdateBlogContentDto
    {
        public string Content { get; set; }
    }
}
