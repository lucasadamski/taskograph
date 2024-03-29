using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using taskograph.Web.Models;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private ITaskRepository _taskRepository;
        private IEntryRepository _entryRepository;

        private readonly ILogger<TaskController> _logger;
        private IConfiguration _configuration;

        public TaskController(ITaskRepository taskRepository, IEntryRepository entryRepository, ILogger<TaskController> logger, IConfiguration configuration)
        {
            _taskRepository = taskRepository;
            _entryRepository = entryRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            TaskViewModel taskVM = new TaskViewModel();
            taskVM.Tasks = _taskRepository.GetAll("asdf").ToList();
            return View(taskVM);
        }

        public IActionResult AddEntry(int taskId, int hours, int minutes)
        {
            Entry entry = new Entry()
            {
                //Id = 1,
                TaskId = 2,
                DurationId = 4,
                DateId = 25
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
