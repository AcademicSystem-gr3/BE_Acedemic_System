namespace A3S.Api.Services.RegisterUserService
{
    public interface IRegisterService
    {
        string GenerateOTP();
        void SaveOTP(string email, string otp);
        bool ValidOTP(string email, string otp);
        bool ValidTimeSpan(string email, string otp);
    }
}
