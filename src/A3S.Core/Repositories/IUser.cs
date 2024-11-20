using A3S.Api.Controllers.Auth;
using A3S.Core.Domain.Identity;
using A3S.Core.SeedWorks;

namespace A3S.Core.Repositories
{
    public interface IUser : IRepositoty<User,Guid>
    {
        void UpdateAsync(User user);
    }
}
