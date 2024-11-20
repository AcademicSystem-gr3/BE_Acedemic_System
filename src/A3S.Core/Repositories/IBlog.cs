using A3S.Core.Domain.Entities;
using A3S.Core.Models.Content;
using A3S.Core.SeedWorks;

namespace A3S.Core.Repositories
{
    public interface IBlog : IRepositoty<Blog,Guid>
    {
       Task<Blog> CreateBlog(Guid userId,string content);
        Task<List<BlogWithUserDto>> GetAllBlogInClassSubject(Guid classId);
    }
}
