using A3S.Core.Domain.Entities;

namespace A3S.Core.Models.Request
{
    public class HomeworkSubmissionRequest
    {
        public Guid HomeworkId { get; set; }
        public StatusHomework status { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string? FeedBack { get; set; }
    }
}
