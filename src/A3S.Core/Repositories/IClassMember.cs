using A3S.Core.Domain.Entities;
using A3S.Core.SeedWorks;

namespace A3S.Core.Repositories
{
    public interface IClassMember : IRepositoty<ClassMember,Guid>
    {
        Task<ClassMember> GetClassNameAsync(ClassMember member);
    }
}
