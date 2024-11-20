using A3S.Core.Domain.Entities;
using A3S.Core.SeedWorks;

namespace A3S.Core.Repositories
{
    public interface IBlock : IRepositoty<Block,Guid>
    {
         Task<Block> GetBlockByName(string name);
    }
}
