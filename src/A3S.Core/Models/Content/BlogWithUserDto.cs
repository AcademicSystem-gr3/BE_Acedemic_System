using A3S.Core.Domain.Entities;

namespace A3S.Core.Models.Content
{
    public class BlogWithUserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserAvatar {  get; set; }
       public Blog Blog { get; set; }

    }
}
