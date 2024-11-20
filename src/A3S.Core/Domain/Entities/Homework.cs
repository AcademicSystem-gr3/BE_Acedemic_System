using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Domain.Entities
{
    public class Homework
    {
        [Key]
        public required Guid HomeworkId { get; set; }
        public required Guid ClassId { get; set; }
        public required Guid CreateBy { get; set; }
        public string fileName { get; set; }
        [StringLength(250)]
        public string Title { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }

        public StatusAssigntment? status { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }

        public virtual ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }
    }
    public enum StatusAssigntment
    {
        Progress,
        Deadline,
        Expired
    }
}
