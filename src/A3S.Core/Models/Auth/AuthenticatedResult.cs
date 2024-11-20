using A3S.Core.Domain.Identity;

namespace A3S.Core.Models.Auth
{
    public class AuthenticatedResult : User
    {
        //public List<string> Roles { get; set; } = new List<string>();
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
