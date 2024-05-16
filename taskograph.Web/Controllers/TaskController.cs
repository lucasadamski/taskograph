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
using System.Threading.Tasks;

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
                GroupId = (taskVM.GroupId == UNASSIGNED_INT ? null : taskVM.GroupId),
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
            ReadUnnasignedTasksSelectedItems(taskVM);
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
            if (taskVM.TaskId != null && taskVM.TaskId != UNASSIGNED_INT)
            {
                Task task = _taskRepository.Get((int)taskVM.TaskId);
                task.GroupId = group.Id;
                _taskRepository.Edit(task);
            }
            return ConfigTasks();
        }

        public IActionResult EditTask(int id)
        {
            TaskViewModel task = new TaskViewModel();
            task.Task = _taskRepository.Get(id);
            task.EditTask = true;
            task.IsFormForTask = true;
            task.Name = task.Task.Name;
            task.TaskId = id;
            ReadColorsSelectedItems(task);
            ReadGroupsSelectedItems(task);
            return View("AddTask", task);
        }

        [HttpPost]
        public IActionResult EditTask(TaskViewModel task)
        {
            Task taskToEdit = _taskRepository.Get((int)task.TaskId);
            taskToEdit.Name = task.Name;
            taskToEdit.GroupId = task.GroupId;
            taskToEdit.ColorId = task.ColorId;
            _taskRepository.Edit(taskToEdit);
            return ConfigTasks();
        }
        public IActionResult EditGroup(int id)
        {
            TaskViewModel task = new TaskViewModel();
            Group group = new Group();
            group = _groupRepository.Get(id);
            task.Name = group.Name;
            task.TaskId = id; //group Id
            task.ColorId = group.ColorId;
            task.IsFormForTask = false;
            task.EditGroup = true;
            task.TasksIdsAssignedToGroup = _groupRepository.GetAssignedTasksIds(id).ToList();  //View will check those Ids against TasksSI to determine if checkbox will be checked
            ReadUnnasignedTasksSelectedItems(task);
            task.TasksSI.AddRange(GetAssignedTasks(id));    //Add assigned tasks to group, thier checkboxes should be checked in view
            ReadColorsSelectedItems(task);
            return View("AddTask", task);
        }

        [HttpPost]
        public IActionResult EditGroup(TaskViewModel task)
        {
            Group groupToEdit = _groupRepository.Get((int)task.TaskId);
            groupToEdit.Name = task.Name;
            groupToEdit.ColorId = task.ColorId;

            task.TasksIdsAssignedToGroup = _groupRepository.GetAssignedTasksIds((int)task.TaskId).ToList();
            
            //check tasksIdsAssigned against AddedTasksIds, if contains then do nothing and remove it from Added,
            //if don't contain remove GroupId from that Task.
            foreach (int taskId in task.TasksIdsAssignedToGroup)
            {
                if (task.AddedTasksIdsToGroup.Contains(taskId))
                {
                    task.AddedTasksIdsToGroup.Remove(taskId);
                }
                else
                {
                    Task taskToRemoveGroupIdFrom = _taskRepository.Get(taskId);
                    taskToRemoveGroupIdFrom.GroupId = null;
                    _taskRepository.Edit(taskToRemoveGroupIdFrom);
                }
            }
            //for all remaining TaskIds in AddedTasksIdsToGroup, add them to Group
            foreach (int taskId in task.AddedTasksIdsToGroup)
            {
                Task taskToAddeGroupIdTo = _taskRepository.Get(taskId);
                taskToAddeGroupIdTo.GroupId = task.TaskId;
                _taskRepository.Edit(taskToAddeGroupIdTo);
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
            task.Groups.Add(new SelectListItem() { Text = UNASSIGNED, Value = UNASSIGNED_INT.ToString() });

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

        private List<SelectListItem> GetAssignedTasks(int groupId)
        {
            return _groupRepository.GetTasks(groupId)
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                })
                .ToList();
        }

        private void ReadUnnasignedTasksSelectedItems(TaskViewModel task)
        {
            string _userId = GetIdentityUserId();
            task.TasksSI = _taskRepository.GetAllUnassigned(_userId)
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                })
                .ToList();
            task.TasksSI.Add(new SelectListItem() { Text = UNASSIGNED, Value = UNASSIGNED_INT.ToString() });

        }

        public void DeleteTask(int id)
        {
            Task task = _taskRepository.Get(id);
            _taskRepository.Delete(task);
        }
    }
}
