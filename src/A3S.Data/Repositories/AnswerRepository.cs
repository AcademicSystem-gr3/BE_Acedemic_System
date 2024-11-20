using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;

namespace A3S.Data.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer, Guid>, IAnswer
    {
        public AnswerRepository(A3SContext context) : base(context)
        {
        }
    }
}
