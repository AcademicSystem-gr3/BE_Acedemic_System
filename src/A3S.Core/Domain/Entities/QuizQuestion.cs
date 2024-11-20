using System.ComponentModel.DataAnnotations.Schema;

namespace A3S.Core.Domain.Entities
{
    public class QuizQuestion
    {
        public required Guid QuizId { get; set; }
        public required Guid QuestionId { get; set; }

        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}
