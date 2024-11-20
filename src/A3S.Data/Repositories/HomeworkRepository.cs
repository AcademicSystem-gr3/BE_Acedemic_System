using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;

namespace A3S.Data.Repositories
{
    public class HomeworkRepository : RepositoryBase<Homework, Guid>, IHomework
    {
        public HomeworkRepository(A3SContext context) : base(context)
        {
        }
    }
}
