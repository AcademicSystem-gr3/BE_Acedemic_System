namespace A3S.Core.Models.Request
{
    public class FolderRequest
    {
        public string FolderName { get; set; }
        public string OwnerId { get; set; }
        public string Category {  get; set; }
        public string? ParentId { get; set; }
    }

}
