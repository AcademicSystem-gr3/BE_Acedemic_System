using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace A3S.Data.Repositories
{
    public class ClassSubjectRepository : RepositoryBase<ClassSubject, Guid>, IClassSubject
    {
        public ClassSubjectRepository(A3SContext context) : base(context)
        {
        }

        public async Task<List<string>> GetSubjectsInClass(string className)
        {
            return await _context.Classes
                .Include(c => c.ClassSubjects)
                .ThenInclude(cs => cs.Subject)
                .Where(c => c.Name == className)
                .SelectMany(c => c.ClassSubjects.Select(cs => cs.Subject.SubjectName))
                .ToListAsync();
        }
    }
}
