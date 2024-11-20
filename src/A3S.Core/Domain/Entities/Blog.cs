using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using A3S.Core.Domain.Identity;

namespace A3S.Core.Domain.Entities
{
    public class Blog
    {
        [Key]
        public required Guid BlogId { get; set; }
        public required Guid CreatorId { get; set; }
        public long View { get; set; }
        public long Status { get; set; }
        [StringLength(250)]
        public string Content { get; set; }
        public int NumLike { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }
        public virtual ICollection<CommentBlog> CommentBlogs { get; set; }
        public virtual ICollection<ClassBlog> ClassBlogs { get; set; }
    }
}
