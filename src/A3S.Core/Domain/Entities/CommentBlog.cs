using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace A3S.Core.Domain.Entities
{
    public class CommentBlog
    {
        [Key]
        public required Guid CommentID { get; set; }
        public required Guid BlogID { get; set; }
        public required Guid CreatorId { get; set; }
        public Guid? RepCommentID { get; set; }
        public string Content { get; set; }
        public int NumLike { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("RepCommentID")]
        public virtual CommentBlog ParentComment { get; set; }
        [ForeignKey("BlogID")]
        public virtual Blog Blog { get; set; }
    }
}
