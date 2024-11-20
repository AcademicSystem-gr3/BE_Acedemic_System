using A3S.Core.Domain.Entities;
using A3S.Core.SeedWorks;

namespace A3S.Core.Repositories
{
    public interface IClassSubject : IRepositoty<ClassSubject,Guid>
    {
        Task<List<string>> GetSubjectsInClass(string className);
    }
}
