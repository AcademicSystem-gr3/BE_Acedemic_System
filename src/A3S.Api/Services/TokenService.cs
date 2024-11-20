using A3S.Core.Models.Content;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using A3S.Core.ConfigOptions;

namespace A3S.Api.Service
{
    public class TokenService : ITokenService
    {
        private readonly JwtTokenSettings _jwtTokenSetting;

        public TokenService(IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            _jwtTokenSetting = jwtTokenSettings.Value;
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSetting.Key));
            var signinCredentials = new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256);
            var tokenoption = new JwtSecurityToken(
                    issuer: _jwtTokenSetting.Issuer,
                    audience: _jwtTokenSetting.Issuer,
                    claims:claims,
                    expires: DateTime.Now.AddMinutes(_jwtTokenSetting.ExpireInMinutes),
                    signingCredentials:signinCredentials
                    );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenoption);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
           var randomNumber = new byte[32];

            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSetting.Key)),
                ValidateLifetime = false,
            };
            var TokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = TokenHandler.ValidateToken(token,TokenValidationParameters,out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken != null &&
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                throw new SecurityTokenExpiredException("Invalid token");
            }
            return principal;
        }

        public void SetTokensInsideCookie(UserDto user, HttpContext context)
        {
            context.Response.Cookies.Append("refreshToken", user.RefreshToken,
                  new CookieOptions
                  {
                      Expires = DateTimeOffset.UtcNow.AddDays(30),
                      HttpOnly = true,
                      IsEssential = true,
                      Secure = false,
                      SameSite = SameSiteMode.None
                  });
        }
    }
}
