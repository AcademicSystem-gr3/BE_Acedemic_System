using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Domain.Entities
{
    public class HomeworkSubmission
    {
        [Key]
        public required Guid SubmissionId { get; set; }
        public required Guid HomeworkId { get; set; }
        public required Guid UserId { get; set; }
        public StatusHomework status { get; set; }
        public DateTime SubmittedAt { get; set; }
        [StringLength(250)]
        public string? FeedBack { get; set; }

        [ForeignKey("HomeworkId")]
        public virtual Homework Homework { get; set; }
    }
    public enum StatusHomework
    {
        Done,
        Late
    }
}
