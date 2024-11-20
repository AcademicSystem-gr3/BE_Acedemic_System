using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Data.Repositories
{
    public class ClassBlogRepository : RepositoryBase<ClassBlog, Guid>, IClassBlog
    {
        public ClassBlogRepository(A3SContext context) : base(context)
        {
        }

        public async Task<ClassBlog> CreateClassBlog(Guid blogId, Guid classId)
        {
            Class classFound = await _context.Classes.Where(c => c.ClassId == classId).FirstOrDefaultAsync();
            ClassBlog classBlog = new ClassBlog { BlogId = blogId, ClassId=classFound.ClassId };
            return classBlog;
        }

        public async Task<ClassBlog> GetBlogsInClass(Guid classId)
        {
           var blogInClass = await _context.ClassBlogs.Where(c=>c.ClassId == classId).Include(c=>c.Blog).FirstOrDefaultAsync();
            return blogInClass;
        }
    }
}
