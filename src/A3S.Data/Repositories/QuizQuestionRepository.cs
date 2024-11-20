using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;

namespace A3S.Data.Repositories
{
    public class QuizQuestionRepository : RepositoryBase<QuizQuestion, Guid>, IQuizQuestion
    {
        public QuizQuestionRepository(A3SContext context) : base(context)
        {
        }
    }
}
