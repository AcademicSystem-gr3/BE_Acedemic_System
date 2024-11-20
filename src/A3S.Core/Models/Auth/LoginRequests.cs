namespace A3S.Core.Models.Auth
{
    public class LoginRequests
    {
        public required string Provider {  get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Fullname { get; set; }
        public string? Avartar { get; set; }
        public string? Address { get; set; }
        public string? IdToken { get; set; }
    }
}
