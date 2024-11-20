namespace A3S.Api.Extesions
{
    public static class GetTheme
    {
        public static string GetImageTheme()
        {
            var listImgTheme = new List<string>()
            {
                "https://www.gstatic.com/classroom/themes/img_learnlanguage.jpg",
                "https://www.gstatic.com/classroom/themes/img_code.jpg",
                "https://www.gstatic.com/classroom/themes/img_reachout.jpg",
                "https://www.gstatic.com/classroom/themes/img_breakfast.jpg",
                "https://www.gstatic.com/classroom/themes/img_theatreopera.jpg",
                "https://www.gstatic.com/classroom/themes/img_bookclub.jpg",
                "https://www.gstatic.com/classroom/themes/img_graduation.jpg",
                "https://www.gstatic.com/classroom/themes/img_code.jpg"
            };
            var random = new Random();
            int index = random.Next(listImgTheme.Count); 
            return listImgTheme[index]; 
        }
    }
}
