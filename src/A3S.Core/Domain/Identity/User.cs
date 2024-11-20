using A3S.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A3S.Core.Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        [StringLength(200)]
        public string Fullname { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        public string? Avatar { get; set; }
        public int Status { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? AccessToken { get; set; }

        public Guid? ParentID { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastLogin { get; set; }

        //[ForeignKey("TeacherID")]
        //public virtual StudentSubject TeacherSubject { get; set; }

        public virtual ICollection<Subject> TaughtSubjects { get; set; }

        [ForeignKey("ParentID")]
        public virtual User ParentUser { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<ClassMember> ClassMembers { get; set; }
        public virtual ICollection<FolderShare> FolderShare { get; set; }
    }
}
