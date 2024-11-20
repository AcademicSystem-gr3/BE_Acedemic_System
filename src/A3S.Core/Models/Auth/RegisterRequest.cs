namespace A3S.Api.Controllers.Auth
{
    public class RegisterRequest
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Otp { get; set;}

    }
}
