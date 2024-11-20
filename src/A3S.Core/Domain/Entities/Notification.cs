using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using A3S.Core.Domain.Identity;

namespace A3S.Core.Domain.Entities
{
    public class Notification
    {
        [Key]
        public required Guid NotificationId { get; set; }
        public required Guid RecevieUserId { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Message { get; set; }
        [StringLength(200)]
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public long Subject { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("RecevieUserId")]
        public virtual User User { get; set; }
    }
}
