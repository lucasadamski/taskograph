using taskograph.Models.Tables;

namespace taskograph.Models.DTOs
{
    public class RegularTargetDTO
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public long TargetDuration { get; set; }
        public long PerTimeframeDuration { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? Deleted { get; set; }

    }
}

