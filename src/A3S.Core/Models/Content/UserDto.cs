using A3S.Core.Domain.Identity;
using AutoMapper;

namespace A3S.Core.Models.Content;

public class UserDto
{
    public string Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string Avatar { get; set; }
    public string PhoneNumber { get; set; }
    public int Status { get; set; }
    public List<string> Roles { get; set; }
    public string Provider {  get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public string? AccessToken { get; set; }
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>();
        }
    }
}
