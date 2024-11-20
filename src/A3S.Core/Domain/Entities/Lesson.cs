using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace A3S.Core.Domain.Entities
{
    public class Lesson
    {
        [Key]
        public required Guid LessionId { get; set; }
        [StringLength(100)]
        public string LessionName { get; set; }
        public required Guid ClassId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
