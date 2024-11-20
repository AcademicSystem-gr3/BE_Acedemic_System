using A3S.Core.Domain.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace A3S.Core.Domain.Entities
{
    public class ClassMember
    {
        public required Guid ClassId { get; set; }
        public required Guid UserId { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
