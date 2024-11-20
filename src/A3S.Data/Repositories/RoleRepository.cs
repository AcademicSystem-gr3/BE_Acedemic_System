using A3S.Core.Domain.Identity;
using A3S.Core.Repositories;
using A3S.Core.SeedWorks;
using A3S.Data.SeedWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Data.Repositories
{
    public class RoleRepository : RepositoryBase<Role, Guid>,IRole
    {
        public RoleRepository(A3SContext context) : base(context)
        {
        }
    }
}
