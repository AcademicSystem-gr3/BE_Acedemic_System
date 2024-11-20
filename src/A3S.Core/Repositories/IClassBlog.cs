using A3S.Core.Domain.Entities;
using A3S.Core.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Repositories
{
    public interface IClassBlog : IRepositoty<ClassBlog,Guid>
    {
        Task<ClassBlog> CreateClassBlog(Guid blogId, Guid classId);
        Task<ClassBlog> GetBlogsInClass(Guid classId);
    }
}
