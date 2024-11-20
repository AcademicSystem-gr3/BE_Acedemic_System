namespace A3S.Core.Models.Auth
{
    public class UpdateProfileRequest
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
    }
}
