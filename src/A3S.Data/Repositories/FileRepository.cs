using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;

namespace A3S.Data.Repositories
{
    public class FileRepository : RepositoryBase<FileContent, Guid>, IFile
    {
        public FileRepository(A3SContext context) : base(context)
        {
        }
    }
}
