using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using Microsoft.AspNetCore.Identity;


namespace A3S.Data.Repositories
{
    public class UserLoginRepository : RepositoryBase<IdentityUserLogin<Guid>, Guid>, IUserLogin
    {
        public UserLoginRepository(A3SContext context) : base(context)
        {
        }
    }
}
