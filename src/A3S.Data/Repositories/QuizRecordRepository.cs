using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;

namespace A3S.Data.Repositories
{
    public class QuizRecordRepository : RepositoryBase<QuizRecord, Guid>, IQuizRecord
    {
        public QuizRecordRepository(A3SContext context) : base(context)
        {
        }
    }
}
