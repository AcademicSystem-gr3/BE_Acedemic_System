using A3S.Api.Service;
using A3S.Core.Domain.Identity;
using A3S.Core.Models.Auth;
using A3S.Core.Models.Content;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace A3S.Api.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public TokensController(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<AuthenticatedResult>> Refresh(TokenRequest tokenRequest)
        {
            if (tokenRequest is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenRequest.AccessToken;
            string refreshToken = Request.Cookies["X-Refresh-Token"];

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null || principal.Identity == null || principal.Identity.Name == null)
                return BadRequest("Invalid token");

            var userName = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.AccessToken = newAccessToken;
            //Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions()
            //{
            //    HttpOnly = true,
            //    SameSite = SameSiteMode.None, 
            //    Secure = true 
            //});
            await _userManager.UpdateAsync(user);

            return Ok(new 
            {
                Access_Token = newAccessToken,           
            });
        }
        [HttpGet]
        [Route("profile")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserWithAccessToken()
        {
            string userId = User.FindFirst("id")?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            UserDto userDto  = _mapper.Map<User,UserDto>(user);
            var roles = await _userManager.GetRolesAsync(user);
            userDto.Roles = roles.ToList();
            return new OkObjectResult(new
            {
                data = userDto,
            });
        }
        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
