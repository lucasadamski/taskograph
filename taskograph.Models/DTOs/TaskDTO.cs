using taskograph.Models;
using taskograph.Models.Tables;

namespace taskograph.Web.Models.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Color { get; set; }
        public Duration TotalDurationToday { get; set; }
    }
}
