using A3S.Core.Domain.Entities;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace A3S.Data.Repositories
{
    public class BlockRepository : RepositoryBase<Block, Guid>, IBlock
    {
        public BlockRepository(A3SContext context) : base(context)
        {
        }
        public async Task<Block> GetBlockByName (string name)
        {
            try
            {
                Block block = await _context.Blocks.Where(b=>b.Name == name).FirstOrDefaultAsync();
                return block;

            }catch (Exception ex)
            {
                return null;
            }
        }
    }
}
