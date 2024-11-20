using System.ComponentModel.DataAnnotations.Schema;

namespace A3S.Core.Domain.Entities
{
    public class ClassBlog
    {
        public required Guid ClassId { get; set; }
        public required Guid BlogId { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }
    }
}
