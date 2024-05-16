using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata.Ecma335;
using taskograph.Models.Tables;
using taskograph.Web.Models.DTOs;
using Task = taskograph.Models.Tables.Task;

namespace taskograph.Web.Models
{
    public class TaskViewModel
    {
        public List<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
        public List<Duration> Durations { get; set; } = new List<Duration>();
        public Task Task { get; set; }
        public Group MyProperty { get; set; }
        /*public List<Group> Groups { get; set; } = new List<Group>();
        public List<Color> Colors { get; set; } = new List<Color>();*/
        public int? GroupId { get; set; }
        public int? ColorId { get; set; }
        public int? TaskId { get; set; }
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> Colors { get; set; }
        public List<SelectListItem> TasksSI { get; set; }
        public string Name { get; set; } = "";
        public bool IsFormForTask { get; set; }
        public bool EditTask { get; set; } = false; //tells AddTask.cshtml that we want to EditTask
        public bool EditGroup{ get; set; } = false; //tells AddTask.cshtml that we want to EditGroup
        public List<int> TasksIdsAssignedToGroup { get; set; } = new List<int>(); //AddTask.cshtml returns selected Tasks for certain Group (list might be empty)
        public List<int> AddedTasksIdsToGroup { get; set; } = new List<int>();   //EditGroup view will return Tasks Ids that are added or removed from certain group. Then controller will tell those tasks to add/delete GroupId from them

    }
}
