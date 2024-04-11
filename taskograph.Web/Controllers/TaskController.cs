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

            if (_configuration.GetValue<bool?>("ConnectUserIdWithExistingTaskEntries") ?? false)
            {
                _taskRepository.DEBUG_ONLY_AssignUserIdToAllTables(_userId);    //****   Debug only!!!  ****
            }             

            //Displays Tasks in table, first load Tasks from DB, then convert it to TaskDTO 
            TaskViewModel taskVM = new TaskViewModel();
            taskVM.Tasks = _taskRepository.GetAllTaskDTOs(_userId).ToList();

            //used for populating Views DropDown with predefined times eg: 00:10, 00:30, 01:00
            taskVM.Durations = _durationRepository.GetFirst(15).ToList();

            return View(taskVM);
        }

        public IActionResult AddEntry(int taskId, int durationId)
        {
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Entry entry = new Entry()
            {
                TaskId = taskId,
                DurationId = durationId,
                Created = DateTime.Now
            };

            _entryRepository.Add(entry);

            TaskViewModel taskVM = new TaskViewModel();
            taskVM.Tasks = _taskRepository.GetAllTaskDTOs(_userId).ToList();

            return View("Index", taskVM);
        }
    }
}
