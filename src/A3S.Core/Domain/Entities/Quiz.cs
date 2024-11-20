using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Domain.Entities
{
    public class Quiz
    {
        [Key]
        public required Guid QuizId { get; set; }
        [StringLength(100)]
        public string QuizName { get; set; }
        public float Duration { get; set; }
        public float? PassRate { get; set; }
        public required Guid LessonId { get; set; }
        public bool Status { get; set; }
        public required Guid CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<QuizRecord> QuizRecords { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
