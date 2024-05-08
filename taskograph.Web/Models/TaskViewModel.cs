﻿using Microsoft.AspNetCore.Mvc.Rendering;
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

    }
}
