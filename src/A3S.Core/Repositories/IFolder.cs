using A3S.Core.Domain.Entities;
using A3S.Core.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Core.Repositories
{
    public interface IFolder : IRepositoty<Folder,Guid>
    {
        Task<List<string>> GetFolderCategoryAsync(Guid id);
        Task<List<Folder>> GetAllFolderContent(Guid id);
    }
}
