using System.ComponentModel.DataAnnotations.Schema;

namespace A3S.Core.Domain.Entities
{
    [Table("Blocks")]
    public class Block
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
