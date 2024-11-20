using A3S.Api.Controllers.Auth;
using A3S.Core.Domain.Identity;
using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Data.Repositories
{
    public class UserRepository : RepositoryBase<User, Guid>, IUser
    {
        public UserRepository(A3SContext context) : base(context)
        {
        }

        public void UpdateAsync(User user)
        {
            _context.Users.Update(user);
        }
    }
}
