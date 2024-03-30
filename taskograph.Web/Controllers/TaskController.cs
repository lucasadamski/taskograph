using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using taskograph.Web.Models;
using taskograph.Web.Models.DTOs;

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

            TaskViewModel taskVM = new TaskViewModel();
            taskVM.Tasks = _taskRepository.GetAll(_userId).ToList();

            //used for populating Views DropDown with predefined times eg: 00:10, 00:30, 01:00
            taskVM.Durations = _durationRepository.GetFirst(15).Select(n => new DurationDTO()
            {
                Id = n.Id,
                Text = n.PrepareText()
            }).ToList();

            return View(taskVM);
        }

        public IActionResult AddEntry(int taskId, int hours, int minutes)
        {
            Entry entry = new Entry()
            {
                TaskId = 2,
                DurationId = 4
            };

            _entryRepository.Add(entry);
            //create new entry
            //create duration

            TaskViewModel taskVM = new TaskViewModel();
            taskVM.Tasks = _taskRepository.GetAll("asdf").ToList();

            return View("Index", taskVM);
        }

    }
}
