using taskograph.Web.Models.DTOs;
using Task = taskograph.Models.Tables.Task;

namespace taskograph.Web.Models
{
    public class TaskViewModel
    {
        public List<Task> Tasks { get; set; } = new List<Task>();
        public List<DurationDTO> Durations { get; set; } = new List<DurationDTO>();
    }
}
