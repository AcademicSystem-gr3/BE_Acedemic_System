using System.ComponentModel.DataAnnotations.Schema;

namespace A3S.Core.Domain.Entities
{
    public class ClassFolder
    {
        public required Guid FolderId { get; set; }
        public required Guid ClassId { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }

        [ForeignKey("FolderId")]
        public virtual Folder Folder { get; set; }
    }
}
