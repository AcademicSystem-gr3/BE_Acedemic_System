namespace A3S.Core.Models.Request
{
    public class LessonRequest
    {
        public string LessionName { get; set; }
        public required Guid ClassId { get; set; }
    }
}
