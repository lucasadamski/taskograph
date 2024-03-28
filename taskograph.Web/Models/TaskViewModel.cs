using Task = taskograph.Models.Tables.Task;

namespace taskograph.Web.Models
{
    public class TaskViewModel
    {
        public List<Task> Tasks { get; set; } = new List<Task>();

    }
}
