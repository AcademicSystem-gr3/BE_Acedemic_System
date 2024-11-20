using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Domain.Entities
{
    public class FileContent
    {
        [Key]
        public required Guid FileId { get; set; }
        [StringLength(100)]
        public string FileName { get; set; }
        public Guid FolderId { get; set; }
        public string FileUrl { get; set; }
        [StringLength(20)]
        public string FileSize { get; set; }
        public required Guid CreatorId { get; set; }
        [StringLength(30)]
        public string Tag { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("FolderId")]
        public virtual Folder Folder { get; set; }
    }
}
