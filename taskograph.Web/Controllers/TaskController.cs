using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using taskograph.Web.Models;
using taskograph.Web.Models.DTOs;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private ITaskRepository _taskRepository;
        private IEntryRepository _entryRepository;
        private IDurationRepository _durationRepository;

        private readonly ILogger<TaskController> _logger;
        private IConfiguration _configuration;


        public TaskController(ITaskRepository taskRepository, IEntryRepository entryRepository, 
            IDurationRepository durationRepository, ILogger<TaskController> logger, IConfiguration configuration)
        {
            _taskRepository = taskRepository;
            _entryRepository = entryRepository;
            _durationRepository = durationRepository;
            _logger = logger;
            _configuration = configuration;
           
        }

        public IActionResult Index()
        {
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _taskRepository.DEBUG_ONLY_TakeAllTasksAndAssignToCurrentUser(_userId);    //****   Debug only!!!  ****

            //Displays Tasks in table, first load Tasks from DB, then convert it to TaskDTO 
            TaskViewModel taskVM = new TaskViewModel();
            List<Task> tasksList = _taskRepository.GetAll(_userId).ToList();
            taskVM.Tasks = ConvertTasksToDTO(tasksList);

            //used for populating Views DropDown with predefined times eg: 00:10, 00:30, 01:00
            taskVM.Durations = _durationRepository.GetFirst(15).Select(n => new DurationDTO()
            {
                Id = n.Id,
                Text = n.ToString()
            }).ToList();

            return View(taskVM);
        }

        public IActionResult AddEntry(int taskId, int durationId)
        {
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Entry entry = new Entry()
            {
                TaskId = taskId,
                DurationId = durationId
            };

            _entryRepository.Add(entry);
            //create new entry
            //create duration

            TaskViewModel taskVM = new TaskViewModel();
            List<Task> tasksList = _taskRepository.GetAll(_userId).ToList();
            taskVM.Tasks = ConvertTasksToDTO(tasksList);

            return View("Index", taskVM);
        }

        private List<TaskDTO> ConvertTasksToDTO(List<Task> input)
        {
            return input.Select(n => new TaskDTO()
            {
                Id = n.Id,
                Name = n.Name,
                Group = n.Group?.Name ?? NULL_VALUE,
                Color = n.Color?.Name ?? NULL_VALUE,
                TotalDurationToday = (_entryRepository.GetTotalDurationForTask(n.Id, DateTime.Now)).ToString()
            })
                .ToList();
        }

    }
}
