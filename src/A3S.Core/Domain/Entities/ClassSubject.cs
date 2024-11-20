using System.ComponentModel.DataAnnotations.Schema;

namespace A3S.Core.Domain.Entities
{
    public class ClassSubject
    {
        public required Guid ClassId { get; set; }
        public required Guid SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
    }
}
