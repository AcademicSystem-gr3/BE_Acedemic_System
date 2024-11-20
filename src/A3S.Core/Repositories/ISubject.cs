using A3S.Core.Domain.Entities;
using A3S.Core.SeedWorks;

namespace A3S.Core.Repositories
{
    public interface ISubject : IRepositoty<Subject,Guid>
    {
        Task<List<Subject>> GetSubjectsForUserClasses(Guid userId,string className);
    }
}
