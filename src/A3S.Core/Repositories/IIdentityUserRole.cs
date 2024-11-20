using A3S.Core.SeedWorks;
using Microsoft.AspNetCore.Identity;

namespace A3S.Core.Repositories
{
    public interface IIdentityUserRole : IRepositoty<IdentityUserRole<Guid>,Guid>
    {
    }
}
