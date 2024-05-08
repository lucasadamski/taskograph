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
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private ITaskRepository _taskRepository;
        private IEntryRepository _entryRepository;
        private IDurationRepository _durationRepository;
        private IGroupRepository _groupRepository;
        private IColorRepository _colorRepository;
        private IAppUserRepository _appUserRepository;

        private readonly ILogger<TaskController> _logger;
        private IConfiguration _configuration;


        public TaskController(ITaskRepository taskRepository, IEntryRepository entryRepository, 
            IDurationRepository durationRepository, IGroupRepository groupRepository, IColorRepository colorRepository,
            IAppUserRepository appUserRepository, ILogger<TaskController> logger, IConfiguration configuration)
        {
            _taskRepository = taskRepository;
            _entryRepository = entryRepository;
            _durationRepository = durationRepository;
            _groupRepository = groupRepository;
            _colorRepository = colorRepository;
            _appUserRepository = appUserRepository;
            _logger = logger;
            _configuration = configuration;
           
        }

        private string? GetIdentityUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);



        public IActionResult Index()
        {
            string _userId = GetIdentityUserId();

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

        public IActionResult AddEntry(int taskId, long minutes)
        {
            string _userId = GetIdentityUserId();

            _entryRepository.Add(taskId, minutes, DateTime.Now);

            TaskViewModel taskVM = new TaskViewModel();
            taskVM.Tasks = _taskRepository.GetAllTaskDTOs(_userId).ToList();

            return View("Index", taskVM);
        }

        public IActionResult ConfigTasks()
        {
            string _userId = GetIdentityUserId();
            ConfigTasksViewModel configTasksVM = new ConfigTasksViewModel();
            configTasksVM.Tasks = _taskRepository.GetAllTaskDTOs(_userId).ToList();
            configTasksVM.Groups = _groupRepository.GetAll(_userId).ToList();

            return View("ConfigTasks", configTasksVM);
        }

        public IActionResult AddTask()
        {
            string _userId = GetIdentityUserId();
            TaskViewModel taskVM = new TaskViewModel();
            ReadGroupsSelectedItems(taskVM);
            ReadColorsSelectedItems(taskVM);
            taskVM.IsFormForTask = true;
            return View("AddTask", taskVM);
        }

       

        [HttpPost]
        public IActionResult AddTask(TaskViewModel taskVM)
        {
            string _userId = GetIdentityUserId();
            Task task = new Task()
            {
                Name = taskVM.Name,
                GroupId = taskVM.GroupId,
                ColorId = taskVM.ColorId,
                AppUserId = _appUserRepository.GetId(_userId)
            };
            _taskRepository.Add(task);
            return ConfigTasks();
        }

        public IActionResult AddGroup()
        {
            string _userId = GetIdentityUserId();
            TaskViewModel taskVM = new TaskViewModel();
            ReadColorsSelectedItems(taskVM);
            ReadTasksSelectedItems(taskVM);
            taskVM.IsFormForTask = false;
            return View("AddTask", taskVM);
        }

        [HttpPost]
        public IActionResult AddGroup(TaskViewModel taskVM)
        {
            string _userId = GetIdentityUserId();

            int tempUserId = _appUserRepository.GetId(_userId);

            Group group = new Group()
            {
                Name = taskVM.Name,
                ColorId = taskVM.ColorId,
                AppUserId = _appUserRepository.GetId(_userId)
            };
            _groupRepository.Add(group);
            if (taskVM.TaskId != null)
            {
                Task task = _taskRepository.Get((int)taskVM.TaskId);
                task.GroupId = group.Id;
                _taskRepository.Edit(task);
            }
            return ConfigTasks();
        }

        private void ReadGroupsSelectedItems(TaskViewModel task)
        {
            string _userId = GetIdentityUserId();
            task.Groups = _groupRepository.GetAll(_userId)
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                })
                .ToList();
        }
        private void ReadColorsSelectedItems(TaskViewModel task)
        {
            string _userId = GetIdentityUserId();
            task.Colors = _colorRepository.GetAll()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                })
                .ToList();
        }

        private void ReadTasksSelectedItems(TaskViewModel task)
        {
            string _userId = GetIdentityUserId();
            task.TasksSI = _taskRepository.GetAll(_userId)
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                })
                .ToList();
        }
    }
}
