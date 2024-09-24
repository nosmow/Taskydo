namespace Taskydo.Entities
{
    public class Step
    {
        public Guid id { get; set; }
        public int taskId { get; set; }
        public Task Task { get; set; }
        public string description { get; set; }
        public bool isDone { get; set; }
        public int order { get; set; }
    }
}
