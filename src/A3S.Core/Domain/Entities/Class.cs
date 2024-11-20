using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using A3S.Core.Domain.Identity;

namespace A3S.Core.Domain.Entities
{
    public class Class
    {
        [Key]
        public required Guid ClassId { get; set; }
        [StringLength(100)]
        public required string Name { get; set; }
        public Guid CreatorId { get; set; }

        public string ClassCode { get; set; }
        public string ImageThemes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Guid BlockId { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        [ForeignKey("BlockId")]
        public virtual Block Block { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<ClassBlog> ClassBlogs { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public virtual ICollection<ClassMember> ClassMembers { get; set; }
        public virtual ICollection<ClassFolder> ClassFolders { get; set; }
    }
}
