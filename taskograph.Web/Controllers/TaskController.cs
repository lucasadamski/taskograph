using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Models;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private ITaskRepository _taskRepository;
        
        private readonly ILogger<TaskController> _logger;
        private IConfiguration _configuration;

        public TaskController(ITaskRepository taskRepository, ILogger<TaskController> logger, IConfiguration configuration)
        {
            _taskRepository = taskRepository;           
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            TaskViewModel taskVM = new TaskViewModel();
            taskVM.Tasks = _taskRepository.GetAll("asdf").ToList();
            return View(taskVM);
        }
    }
}
