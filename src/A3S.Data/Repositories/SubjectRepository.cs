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
    public class SubjectRepository : RepositoryBase<Subject, Guid>, ISubject
    {
        public SubjectRepository(A3SContext context) : base(context)
        {
        }

        public async Task<List<Subject>> GetSubjectsForUserClasses(Guid userId, string className)
        {
            var subjects = await _context.ClassMembers
                 .Where(cm => cm.UserId == userId)
                 .Where(cm => cm.Class.Name == className) 
                 .Select(cm => cm.Class)
                 .SelectMany(c => c.ClassSubjects)
                 .Select(cs => cs.Subject)
                 .Distinct()
                 .ToListAsync();

            return subjects;
        }
    }
}
