using A3S.Core.Domain.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace A3S.Core.Domain.Entities
{
    public class FolderShare
    {
        public Guid FolderId { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("FolderId")]
        public virtual Folder Folder { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
