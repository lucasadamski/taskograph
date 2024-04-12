using taskograph.Models.Tables;

namespace taskograph.Models.DTOs
{
    public class RegularTargetDTO
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public Duration TargetDuration { get; set; }
        public Duration PerTimeframeDuration { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? Deleted { get; set; }

    }
}

