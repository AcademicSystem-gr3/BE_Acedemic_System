namespace A3S.Core.Models.Request
{
    public class QuizRequest
    {
        public string QuizName { get; set; }
        public float Duration { get; set; }
        public float? PassRate { get; set; }
        public required Guid LessonId { get; set; }
        public required Guid CreatorId { get; set; }
        public List<ListQuestionAndAnswer> ListQuestionAndAnswers { get; set; }
    }

    public class ListQuestionAndAnswer
    {
        public string Question { get; set;}
        public List<string> ListAnswers { get; set; }
        public int IsCorrect {  get; set;}
    }

}
