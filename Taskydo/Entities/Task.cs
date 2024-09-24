using System.ComponentModel.DataAnnotations;

namespace Taskydo.Entities
{
    public class Task
    {
        public int id { get; set; }
        [StringLength(250)]
        [Required]
        public string title { get; set; }
        public string description { get; set; }
        public int order { get; set; }
        public DateTime creationDate { get; set; }
        public List<Step> steps { get; set; }
        public List<AttachedFile> attachedFiles { get; set; }
    }
}
