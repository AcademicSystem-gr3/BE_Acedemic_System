using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using A3S.Core.Domain.Identity;

namespace A3S.Core.Domain.Entities
{
    public class Subject
    {
        [Key]
        public required Guid SubjectId { get; set; }
        [StringLength(20)]
        public string SubjectCode { get; set; }
        [StringLength(100)]
        public string SubjectName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public Guid CreatorBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? TeacherID { get; set; }
        [ForeignKey("TeacherID")]
        public virtual User Teacher { get; set; }
        [ForeignKey("CreatorBy")]
        public virtual User Creator { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
    }
}
