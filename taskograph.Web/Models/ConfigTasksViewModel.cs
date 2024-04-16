using Task = taskograph.Models.Tables.Task;
using Group = taskograph.Models.Tables.Group;
using taskograph.Web.Models.DTOs;

namespace taskograph.Web.Models
{
    public class ConfigTasksViewModel
    {
        public List<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
        public List<Group> Groups { get; set; } = new List<Group>();
    }
}
