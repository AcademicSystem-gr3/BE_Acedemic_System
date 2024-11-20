using A3S.Core.Repositories;
using A3S.Data.SeedWorks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Data.Repositories
{
    public class UserRoleRepository : RepositoryBase<IdentityUserRole<Guid>, Guid>, IIdentityUserRole
    {
        public UserRoleRepository(A3SContext context) : base(context)
        {
        }
    }
}
