using A3S.Api.Services.RegisterUserService;
using A3S.Core.Domain.Identity;
using A3S.Core.Models.Auth;
using A3S.Core.Models.Content;
using A3S.Core.SeedWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace A3S.Api.Controllers.Auth
{
    [Route("api/password")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        public ResetPasswordController(UserManager<User> userManager, IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }
        [HttpPost("verify")]
        public async Task<ActionResult> VerifyMail(VerifyMailRequest mailRequest)
        {
            if(mailRequest == null)
            {
                return BadRequest();
            }
            User user = await _userManager.FindByEmailAsync(mailRequest.Email);
            EmailData emailData;
            if (user == null)
            {
                return NotFound(new
                {
                    message="Địa chỉ email khôn tồn tại!"
                });
            }
            else
            {
                if (user.PasswordHash == null)
                {
                    return BadRequest(new
                    {
                        message = "Mail khôn hợp lệ"
                    });
                }
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //string safeToken = token.Replace("/", "-");
                //string resetLink = $"http://localhost:4200/reset-password/{safeToken}/{mailRequest.Email}";
                string safeToken = HttpUtility.UrlEncode(token);
                string resetLink = $"http://localhost:4200/reset-password/{safeToken}/{HttpUtility.UrlEncode(mailRequest.Email)}";
                string emailContent = $"Click <a href='{resetLink}'>here</a> to reset your password.";
                emailData = new EmailData
                {
                    ToEmail = user.Email,
                    Subject = "Password Reset",
                    Content = emailContent
                };
            }
            await _emailSender.SendEmail(emailData);
            return Ok();
        }
        [HttpPost("reset")]
        public async Task<ActionResult> ResetPassword(ResetPassRequest resetPasswordRequest)
        {
            if (resetPasswordRequest == null)
            {
                return BadRequest();
            }
            User user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email);
            if (user == null)
            {
                return BadRequest();
            }
            string decodedToken = DecodeToken(resetPasswordRequest.Token);
            string decodedEmail = HttpUtility.UrlDecode(resetPasswordRequest.Email);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordRequest.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code == "InvalidToken")
                    {
                        return BadRequest(new { message = "Token không hợp lệ hoặc đã hết hạn." });
                    }
                }
                return BadRequest(new
                {
                    message = "Mật khẩu phải chứa ít nhất một chữ số,chữ cái viết thường,chữ cái viết hoa và tối thiểu 6 kí tự"
                });
            }
            return Ok(new { message = "Password reset successfully" });
        }
        private string DecodeToken(string encodedToken)
        {
            // thay thế khoảng trắng bằng '+'
            string plusReplacedToken = encodedToken.Replace(" ", "+");

            //  sử dụng UrlDecode
            return HttpUtility.UrlDecode(plusReplacedToken);
        }

    }
}
