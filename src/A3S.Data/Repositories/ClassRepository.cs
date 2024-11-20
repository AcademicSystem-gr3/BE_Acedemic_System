using A3S.Core.Domain.Entities;
using A3S.Core.Models.Content;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace A3S.Data.Repositories
{
    public class ClassRepository : RepositoryBase<Class, Guid>, IClass
    {
        public ClassRepository(A3SContext context) : base(context)
        {
        }
        public async Task<List<Class>> GetClassesWithUserAsync(string blockName, Guid userId)
        {
            return await _context.Classes
                                 .Where(c => c.Block.Name == blockName && c.CreatorId == userId) 
                                 .Include(c => c.Creator) 
                                 .Include(c=>c.Block)
                                 .ToListAsync();
        }
        public async Task<Class> GetClassById(string classId, Guid userId)
        {
            return await _context.Classes
                     .Where(c => c.ClassId == Guid.Parse(classId) && c.CreatorId == userId)
                     .Include(c => c.Creator)
                     .Include(c => c.Block)
                     .FirstOrDefaultAsync();
        }

        public async Task<Class> GetClassInBlog(Guid classId)
        {
            return await _context.Classes
                .Where(c => c.ClassId == classId)
                .Include(c => c.ClassBlogs)
                .ThenInclude(c=>c.Blog)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Class>> classesWithSubjects(string className)
        {
            var classesWithSubjects = await _context.Classes
                .Where(c => c.Name == className)
                .Include(c => c.ClassSubjects)
                    .ThenInclude(cs => cs.Subject)
                .ToListAsync();
            return (classesWithSubjects);
        }

        public async Task<List<Class>> GetAllClassStudent(Guid userId)
        {
            return await _context.ClassMembers
                .Where(c => c.UserId == userId)
                .Select(c => c.Class)
                .GroupBy(c => c.Name)
                .Select(g => g.First())
                .ToListAsync();

        }

        public async Task<List<SubjectWithClassDto>> GetSubjectsForUserClasses(Guid userId, string className)
        {
            var subjects = await _context.ClassMembers
                .Where(cm => cm.UserId == userId && cm.Class.Name == className)
                .SelectMany(cm => cm.Class.ClassSubjects, (cm, cs) => new SubjectWithClassDto
                {
                    ClassId = cm.ClassId,
                    Subject = cs.Subject,
                    TeacherName = cs.Subject.Teacher.Fullname,
                    teacherEmail = cs.Subject.Teacher.Email,
                    teacherAvatar = cs.Subject.Teacher.Avatar,
                    imgTheme = GetImageTheme()
                })
                .Distinct()
                .ToListAsync();
            return subjects;
        }
        private string GetImageTheme()
        {
            var listImgTheme = new List<string>()
            {
                "https://www.gstatic.com/classroom/themes/img_learnlanguage.jpg",
                "https://www.gstatic.com/classroom/themes/img_code.jpg",
                "https://www.gstatic.com/classroom/themes/img_reachout.jpg",
                "https://www.gstatic.com/classroom/themes/img_breakfast.jpg",
                "https://www.gstatic.com/classroom/themes/img_theatreopera.jpg",
                "https://www.gstatic.com/classroom/themes/img_bookclub.jpg",
                "https://www.gstatic.com/classroom/themes/img_graduation.jpg",
                "https://www.gstatic.com/classroom/themes/img_code.jpg"
            };
            var random = new Random();
            int index = random.Next(listImgTheme.Count);
            return listImgTheme[index];
        }
    }
}
