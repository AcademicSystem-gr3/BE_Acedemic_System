using A3S.Core.Domain.Entities;
using A3S.Core.Models.Content;
using A3S.Core.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Repositories
{
    public interface IClass : IRepositoty<Class,Guid>
    {
        Task<List<Class>> GetClassesWithUserAsync(string blockName, Guid userId);
        Task<Class> GetClassById(string classId, Guid userId);
        Task<Class> GetClassInBlog(Guid classId);
        Task<List<Class>> GetAllClassStudent(Guid userId);
        Task<List<SubjectWithClassDto>> GetSubjectsForUserClasses(Guid userId, string className);
    }
}
