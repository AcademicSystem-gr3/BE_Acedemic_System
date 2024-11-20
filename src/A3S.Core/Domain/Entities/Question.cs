using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Domain.Entities
{
    public class Question
    {
        [Key]
        public required Guid QuestionId { get; set; }
        [StringLength(100)]
        public string QuestionDetail { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Status { get; set; }
        public required Guid CreatorId { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
