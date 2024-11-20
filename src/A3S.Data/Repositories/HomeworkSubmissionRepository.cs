using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Data.Repositories
{
    public class HomeworkSubmissionRepository : RepositoryBase<HomeworkSubmission, Guid>, IHomeworkSubmission
    {
        public HomeworkSubmissionRepository(A3SContext context) : base(context)
        {
        }
    }
}
