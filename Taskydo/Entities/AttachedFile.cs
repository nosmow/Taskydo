using Microsoft.EntityFrameworkCore;

namespace Taskydo.Entities
{
    public class AttachedFile
    {
        public Guid id { get; set; }
        public int taskId { get; set; }
        public Task task { get; set; }
        [Unicode]
        public string url { get; set; }
        public string title { get; set; }
        public int order { get; set; }
        public DateTime creationDate { get; set; }
    }
}
