using A3S.Api.Service;
using A3S.Core.Domain.Constant;
using A3S.Core.Domain.Identity;
using A3S.Core.Models.Auth;
using A3S.Core.Models.Content;
using A3S.Core.SeedWorks;
using A3S.Data;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;


namespace A3S.Api.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly A3SContext _a3SContext;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService,
            RoleManager<Role> roleManager, IMapper mapper, IUnitOfWork unitOfWork , A3SContext a3SContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _a3SContext = a3SContext;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequests request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request");
            }
            User user = null;
            if(request.Provider == "google")
            {
                user = await _userManager.FindByEmailAsync(request.UserName);
                var googlePayload = await VerifyGoogleToken(request.IdToken);
                if (user == null)
                {
                    Guid userId = Guid.NewGuid();
                    string[] userNameRequest = request.UserName.Split(new char[] { '@' });
                    user = new User()
                    {
                        Id = userId,
                        Fullname = googlePayload.Name,
                        Address = request.Address,
                        Avatar = googlePayload.Picture,
                        Email = request.UserName,
                        NormalizedEmail = googlePayload.Name.ToUpper(),
                        UserName = googlePayload.Email.ToLower(),
                        NormalizedUserName = googlePayload.Email.ToUpper(),
                        Status = 1,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        LockoutEnabled = false,
                    };
                    try
                    {
                        var result = await _userManager.CreateAsync(user);
                        if (!result.Succeeded)
                        {
                            return BadRequest("Failed to create user");
                        }

                    }catch(Exception ex)
                    {

                    }
                    await _unitOfWork.CompleteAsync();
                    var userRole = await _roleManager.Roles.Where(o => o.NormalizedName == "ADMIN").FirstOrDefaultAsync();
                    var userRoleMapping = new IdentityUserRole<Guid>
                    {
                        RoleId = userRole.Id, 
                        UserId = user.Id 
                    };
                    _a3SContext.UserRoles.AddAsync(userRoleMapping);
                    await _unitOfWork.CompleteAsync();
                    var userProvider = new IdentityUserLogin<Guid>
                    {
                        LoginProvider = request.Provider,
                        ProviderKey = googlePayload.Subject,
                        ProviderDisplayName = request.Provider.ToUpper(),
                        UserId = user.Id
                    };
                    await _a3SContext.UserLogins.AddAsync(userProvider);
                    await _unitOfWork.CompleteAsync();
                }
                else
                {
                    var userProvider = await _a3SContext.UserLogins
                     .FirstOrDefaultAsync(x => x.UserId == user.Id && x.LoginProvider == "GOOGLE");
                    if (userProvider.ProviderDisplayName != "GOOGLE")
                    {
                        return new BadRequestObjectResult(new
                        {
                            message = "Email da duoc su dung"
                        });
                    }
                }
            }
            else
            {
                user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null || user.Status != 1 || user.LockoutEnabled)
                {
                    return new UnauthorizedObjectResult(new
                    {
                        message = "User is not authorized due to invalid status or lockout."
                    });
                }

                var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
                if (!result.Succeeded)
                {
                    return Unauthorized();
                }
            }
            var roles = await _userManager.GetRolesAsync(user);
            var permission = await this.GetPermissionsByUserIdAsync(user.Id.ToString());

            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(UserClaims.Id, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(UserClaims.Fullname, user.Fullname),
                new Claim(UserClaims.Roles, string.Join(";", roles)),
                new Claim(UserClaims.Permissions, JsonSerializer.Serialize(permission)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            //DateTime today = DateTime.Now;
            //DateTime answer = today.AddDays(10);
            var accessToken = _tokenService.GenerateAccessToken(claim);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(10);
            user.AccessToken = accessToken;
            await _userManager.UpdateAsync(user);
            var userDto = _mapper.Map<User, UserDto>(user);
            userDto.Roles = roles.ToList();
            var userLogins = await _userManager.GetLoginsAsync(user);

            if (userLogins.Any())
            {
                // Nếu có provider, lấy tên provider đầu tiên
                userDto.Provider = userLogins.First().ProviderDisplayName;
            }
            else
            {
                // Nếu không có provider nào, tức là người dùng đăng nhập bằng tài khoản cục bộ
                userDto.Provider = "CREDENTIAL";
            }
            Response.Cookies.Append("X-Refresh-Token", userDto.RefreshToken, new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None, // Use 'None' if frontend and backend are on different origins
                Secure = true // Secure should be true if you're using HTTPS
            });


            return Ok(new
            {
                data= userDto,
            });
        }
        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { "571984643788-r28k3ecsrajdebppuj21daqebo3hqj2e.apps.googleusercontent.com" }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return payload;
            }
            catch
            {
                return null;
            }
        }
        private async Task<List<string>> GetPermissionsByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var permissions = new List<string>();
            foreach (var roleName in roles)
            {
                if (roleName == RolesConstant.Admin)
                {
                    //var allPermissions = new List<RoleClaimsDto>();
                    //var types = typeof(Permissions).GetTypeInfo().DeclaredNestedTypes;
                    //foreach (var type in types)
                    //{
                    //    allPermissions.GetPermissions(type);
                    //}
                    //permissions.AddRange(allPermissions.Select(x => x.Value));
                    permissions.Add(Permissions.Users.View);
                    permissions.Add(Permissions.Users.Create);
                    permissions.Add(Permissions.Users.Edit);
                    permissions.Add(Permissions.Users.Delete);
                }
                else if (roleName == RolesConstant.Student)
                {
                    permissions.Add(Permissions.Homeworks.View);
                }
                else if (roleName == RolesConstant.Teacher)
                {
                    permissions.Add(Permissions.Users.View);
                    permissions.Add(Permissions.Users.Create);
                    permissions.Add(Permissions.Users.Edit);
                    permissions.Add(Permissions.Users.Delete);
                }
                else if (roleName == RolesConstant.Guest)
                {
                    // Viewer chỉ có quyền xem
                    
                }
                else
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    var claims = await _roleManager.GetClaimsAsync(role);
                    permissions.AddRange(claims.Select(c => c.Value));
                }
            }

            return permissions.Distinct().ToList();
        }
    }
}
