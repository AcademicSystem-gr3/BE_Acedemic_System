using A3S.Core.Domain.Identity;
using A3S.Core.Models.Auth;
using A3S.Core.Models.Content;
using A3S.Core.SeedWorks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace A3S.Api.Controllers.UserApi
{
    [Route("api/user")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public readonly IMapper _mapper;
        public CommonController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<UserDto>> UpdateProfile([FromBody] UpdateProfileRequest updateRequest)
        {
            if (updateRequest == null)
            {
                return BadRequest();
            }
            string userId = User.FindFirst("id")?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }

            user.Fullname = updateRequest.Fullname;
            user.PhoneNumber = updateRequest.Phone;
            user.Address = updateRequest.Address;
            user.Avatar = updateRequest.Avatar;
            _unitOfWork.User.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();
            var userDto = _mapper.Map<User, UserDto>(user);
            return Ok(new
            {
                data = userDto
            });

        }
        [HttpGet]
        [Route("get-user")]
        [Authorize]
        public async Task<ActionResult> GetUserById(string userId)
        {
            if(userId == null)
            {
                return BadRequest();
            }
            var user = _unitOfWork.User.Find(u=>u.Id == Guid.Parse(userId)).FirstOrDefault();
            return Ok(_mapper.Map<User, UserDto>(user));
        }
        [Authorize]
        [HttpPut]
        [Route("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest passwordRequest)
        {
            if (passwordRequest.OldPassword == null || passwordRequest.NewPassword ==null)
            {
                return BadRequest();
            }
            
            string userId = User.FindFirst("id")?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            
            IdentityResult result = await _userManager.ChangePasswordAsync(user, passwordRequest.OldPassword, passwordRequest.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = "Mật khẩu phải chứa ít nhất một chữ số,chữ cái viết thường,chữ cái viết hoa và tối thiểu 6 kí tự"
                });
            }
            return Ok(new
            {
                message="Đổi mật khẩu thành công"
            });
        }
    }
}
