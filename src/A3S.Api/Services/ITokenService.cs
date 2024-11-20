using A3S.Core.Models.Content;
using System.Security.Claims;

namespace A3S.Api.Service
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        void SetTokensInsideCookie(UserDto user, HttpContext context);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
