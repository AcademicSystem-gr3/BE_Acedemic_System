using A3S.Core.Domain.Entities;
using A3S.Core.Models.Content;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace A3S.Data.Repositories
{
    public class BlogRepository : RepositoryBase<Blog, Guid>, IBlog
    {
        public BlogRepository(A3SContext context) : base(context)
        {
        }

        public async Task<Blog> CreateBlog(Guid userId,string content)
        {
            Blog blog = new Blog
            {
                BlogId = Guid.NewGuid(),
                CreatorId = userId,
                Content = content,
                Status = 1,
                View = 0,
                NumLike = 0,
            };

            return blog;
        }

        public async Task<List<BlogWithUserDto>> GetAllBlogInClassSubject(Guid classId)
        {
            return await _context.ClassBlogs
                .Where(cb => cb.ClassId == classId)
                .Select(cb => new BlogWithUserDto
                {
                    Blog = cb.Blog,
                    UserName = cb.Blog.Creator.Fullname,
                    UserEmail = cb.Blog.Creator.Email,
                    UserAvatar = cb.Blog.Creator.Avatar,
                    UserId = cb.Blog.Creator.Id
                })
                .ToListAsync();
        }
    }
}
