using A3S.Core.Domain.Entities;
using A3S.Core.Domain.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Models.Content
{
    public class ClassBlogDto
    {
        public  Guid ClassId { get; set; }
        public  Guid BlogId { get; set; }

        public List<BlogDto> Blogs { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<ClassBlog, ClassBlogDto>().ForMember(dest => dest.Blogs, opt => opt.MapFrom(src => src.BlogId));
                CreateMap<Blog, BlogDto>();
            }
        }
    }
}
