using A3S.Api.Services.RegisterUserService;
using A3S.Core.Domain.Identity;
using A3S.Core.Models.Content;
using A3S.Core.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A3S.Api.Controllers.Auth
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IRegisterService _registerService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterController(UserManager<User> userManager, IMapper mapper,IEmailSender emailSender, IRegisterService registerService
            , RoleManager<Role> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _registerService = registerService;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest("Invalid request");
            }
            if(await _userManager.FindByEmailAsync(registerRequest.Email) != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Email đã tồn tại"
                });
            }
            User userPhoneExist = await _userManager.Users.FirstOrDefaultAsync(o => o.PhoneNumber == registerRequest.Phone);
            if (userPhoneExist != null)
            {
                return new BadRequestObjectResult(new
                {
                    message = "Số điện thoại đã tồn tại"
                });
            }
            string otp = _registerService.GenerateOTP();
            string emailTemplate = await System.IO.File.ReadAllTextAsync("MailTemplate\\MailTemplate.html"); 
            string emailContent = emailTemplate.Replace("{otp}", otp);
            EmailData emailData = new EmailData
            {
                ToEmail = registerRequest.Email,
                Subject = "Your OTP Code",
                Content = emailContent,
                            
            };
            _registerService.SaveOTP(registerRequest.Email,otp);
            await _emailSender.SendEmail(emailData);
            return new OkObjectResult(new
            {
                message = "Mã OTP đã gửi về mail của bạn"
            });
        }
        [HttpPost("verify-otp")]
            public async Task<ActionResult> VerifyOtp([FromBody] RegisterRequest verifyOptionRequest)
            {
                if (!_registerService.ValidOTP(verifyOptionRequest.Email, verifyOptionRequest.Otp))
                {
                    return new BadRequestObjectResult(new
                    {
                        message="Mã OTP không hợp lệ"
                    });
                }
                if(!_registerService.ValidTimeSpan(verifyOptionRequest.Email, verifyOptionRequest.Otp))
                {
                    return new BadRequestObjectResult(new {
                        message = "Mã OTP hết hiều lực"
                    });
                }
                Guid userId = Guid.NewGuid();
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            User user = new User
                {
                    Id = userId,
                    Fullname = verifyOptionRequest.Fullname,
                    Address = verifyOptionRequest.Address,
                    Avatar = null,
                    Email = verifyOptionRequest.Email,
                    PhoneNumber = verifyOptionRequest.Phone,
                    NormalizedEmail = verifyOptionRequest.Email.ToUpper(),
                    UserName = verifyOptionRequest.Email.ToLower(),
                    NormalizedUserName = verifyOptionRequest.Email.ToUpper(),
                    Status = 1,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                };
            user.PasswordHash = passwordHasher.HashPassword(user, verifyOptionRequest.Password);
                _unitOfWork.User.Add(user);
                var userRole = await _roleManager.Roles.Where(o => o.NormalizedName == "STUDENT").FirstOrDefaultAsync();
                var userRoleMapping = new IdentityUserRole<Guid>
                {
                    RoleId = userRole.Id,
                    UserId = user.Id
                };
                _unitOfWork.UserRole.Add(userRoleMapping);

                var userProvider = new IdentityUserLogin<Guid>
                {
                    LoginProvider = "Credential",
                    ProviderKey = userId.ToString(),
                    ProviderDisplayName = "Credential".ToUpper(),
                    UserId = user.Id
                };
                _unitOfWork.UserLogin.Add(userProvider);
                await _unitOfWork.CompleteAsync();
                return new OkObjectResult(new
                {
                    message="Dang ki thanh cong"
                });
            }
        [HttpPost("resend-otp")]
        public async Task<ActionResult> ResendOtp([FromBody] RegisterRequest registerRequest)
        {
            string otp = _registerService.GenerateOTP();
            string emailTemplate = await System.IO.File.ReadAllTextAsync("MailTemplate\\MailTemplate.html");
            string emailContent = emailTemplate.Replace("{otp}", otp);
            EmailData emailData = new EmailData
            {
                ToEmail = registerRequest.Email,
                Subject = "Your OTP Code",
                Content = emailContent,

            };
            _registerService.SaveOTP(registerRequest.Email, otp);
            await _emailSender.SendEmail(emailData);
            return new OkObjectResult(new
            {
                message = "Mã OTP đã gửi về mail của bạn"
            });
        }
    }
}
