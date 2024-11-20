using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Domain.Entities
{
    public class Answer
    {
        [Key]
        public required Guid AnswerId { get; set; }
        public required Guid QuestionId { get; set; }
        [StringLength(100)]
        public string AnswerDetail { get; set; }
        public bool IsCorrect { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}
