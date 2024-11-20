using System.Text;

namespace A3S.Api.Extesions
{
    public static class GenerateRandomClassCode
    {
        public static string GenerateRandomClassCodes()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            var codeBuilder = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                codeBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return codeBuilder.ToString();
        }
    }
}
