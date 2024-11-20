using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace A3S.Data.Repositories
{
    public class FolderRepository : RepositoryBase<Folder, Guid>, IFolder
    {
        public FolderRepository(A3SContext context) : base(context)
        {
        }

        public async Task<List<Folder>> GetAllFolderContent(Guid id)
        {
            List<Folder> listFolderUser = await _context.Folders.Where(u=>u.OwnerId==id).Include(f=>f.Files).ToListAsync();
            return listFolderUser;
        }

        public async Task<List<string>> GetFolderCategoryAsync(Guid id)
        {
            var folder = await _context.Folders.ToListAsync();
            List<string> listCategory = folder.Select(f => f.Category).ToList();
            return listCategory;
        }
    }
}
