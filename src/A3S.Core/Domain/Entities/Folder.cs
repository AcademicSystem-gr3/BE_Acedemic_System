using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using A3S.Core.Domain.Identity;

namespace A3S.Core.Domain.Entities
{
    public class Folder
    {
        [Key]
        public required Guid FolderId { get; set; }
        [StringLength(20)]
        public string FolderName { get; set; }
        public Guid? Parent { get; set; }
        public required Guid OwnerId { get; set; }
        [StringLength(20)]
        public string Category { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("Parent")]
        public virtual Folder ParentFolder { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        public virtual ICollection<FileContent> Files { get; set; }
        public virtual ICollection<ClassFolder> ClassFolders { get; set; }
        public virtual ICollection<FolderShare> FolderShare { get; set; }
    }
}
