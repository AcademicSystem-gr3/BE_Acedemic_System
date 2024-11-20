using Microsoft.AspNetCore.Identity;

namespace A3S.Core.Domain.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public required string DisplayName { get; set; }
    }
}
