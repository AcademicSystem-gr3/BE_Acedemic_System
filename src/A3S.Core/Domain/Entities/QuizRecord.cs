using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Domain.Entities
{
    public class QuizRecord
    {
        [Key]
        public required Guid QuizRecordId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishDate { get; set; }
        public long TimeSpent { get; set; }
        public float Score { get; set; }
        public required Guid UserId { get; set; }
        public required Guid QuizId { get; set; }

        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }
    }
}
