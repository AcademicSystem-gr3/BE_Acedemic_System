namespace A3S.Api.Services.RegisterUserService
{
    public class RegisterService : IRegisterService
    {
        private readonly Dictionary<string, (string otp, DateTime createdAt)> _otpStorage = new Dictionary<string, (string otp, DateTime createdAt)>();
        public string GenerateOTP()
        {
           Random rnd = new Random();
            int randomNumber = rnd.Next(100000, 999990);
            return randomNumber.ToString();
        }

        public void SaveOTP(string email, string otp)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                _otpStorage[email] = (otp, DateTime.Now);
            }
        }

        public bool ValidOTP(string email, string otp)
        {
            if (!string.IsNullOrWhiteSpace(email) && _otpStorage.ContainsKey(email))
            {
                var (storedOtp, createdAt) = _otpStorage[email];
                return otp == storedOtp; // Kiểm tra OTP khớp
            }

            return false;
            // if (_otpStorage.ContainsKey(email))
            // {
            //     var (storedOtp, createdAt) = _otpStorage[email];
            //     DateTime currentTime = DateTime.Now;
            //     if(currentTime -  createdAt <= TimeSpan.FromMinutes(5) && storedOtp==otp)
            //     {
            //         return true;
            //     }
            // }   
            //return false;
        }

        public bool ValidTimeSpan(string email, string otp)
        {
            var (storedOtp, createdAt) = _otpStorage[email];
            DateTime currentTime = DateTime.Now;
            if(currentTime-createdAt <= TimeSpan.FromMinutes(3) && storedOtp==otp)
            {
                return true;
            }
            return false;
        }
        private string GetLastestEmail(string email)
        {
            if(_otpStorage.Count==0)
            {
                return null;
            }
            var lastestEmail = _otpStorage.Where(e=>e.Key == email).OrderByDescending(d=>d.Value.createdAt).FirstOrDefault();
            return lastestEmail.Key;
        }
    }
}
