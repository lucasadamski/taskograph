using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using Microsoft.EntityFrameworkCore;
using taskograph.Models.Tables;
using taskograph.Web.Models.DTOs;

namespace taskograph.EF.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<TaskRepository> _logger;
        private readonly IMapper _mapper;
        private IEntryRepository _entryRepository;
        private IGroupRepository _groupRepository;

        public TaskRepository(TasksContext db, ILogger<TaskRepository> logger, IMapper mapper, IEntryRepository entryRepository,
            IGroupRepository groupRepository)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _entryRepository = entryRepository;
            _groupRepository = groupRepository;
        }

        public bool Add(Task task)
        {
            try
            {
                task.Created = DateTime.Now;
                _db.Tasks.Add(task);
                _db.SaveChanges();
                _logger.LogDebug($"TaskRepository: Add {task.Name}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: Add {task.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Delete(Task task)
        {
            try
            {
                task.Deleted = DateTime.Now;
                task.GroupId = null;
                _db.Tasks.Update(task);
                _db.SaveChanges();
                _logger.LogDebug($"TaskRepository: Delete {task.Name}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: Delete {task.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Edit(Task task)
        {
            try
            {
                task.LastUpdated = DateTime.Now;
                _db.Tasks.Update(task);
                _db.SaveChanges();
                _logger.LogDebug($"TaskRepository: Edit {task.Name}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: Edit {task.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public IEnumerable<Task> GetAll(string userId)
        {
            IEnumerable<Task> result;
            try
            {
                result = _db.Tasks.Include(n => n.Group) //TODO add UserId column
                    .Include(n => n.ApplicationUser)
                    .Where(n => n.Deleted == null)
                    .Where(n => n.ApplicationUserId == userId)
                    .ToList();
                _logger.LogDebug($"TaskRepository: GetAllTasks: UserID {userId} Message: {DATABASE_OK}");

            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: GetAllTasks: UserID {userId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Task>();
            }

            return result;
        }
        public IEnumerable<Task> GetAllUnassigned(string userId)
        {
            IEnumerable<Task> result;
            try
            {
                result = _db.Tasks.Include(n => n.Group) //TODO add UserId column
                    .Include(n => n.ApplicationUser)
                    .Where(n => n.GroupId == null)
                    .Where(n => n.Deleted == null)
                    .Where(n => n.ApplicationUserId == userId)
                    .ToList();
                _logger.LogDebug($"TaskRepository: GetAllTasks: UserID {userId} Message: {DATABASE_OK}");

            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: GetAllTasks: UserID {userId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Task>();
            }

            return result;
        }

        public IEnumerable<TaskDTO> GetAllTaskDTOs(string userId)
        {
            IEnumerable<TaskDTO> result;
            
            try
            {
                result = GetAll(userId)
                    .Select(n => new TaskDTO()
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Group = n.Group?.Name ?? NULL_VALUE,
                        TotalDurationToday = (_entryRepository.GetTotalDurationForTask(n.Id, DateTime.Now))
                    })
                 .ToList();
                _logger.LogDebug($"GetAllTaskDTOs: UserID {userId} Message: {DATABASE_OK}");

            }
            catch (Exception e)
            {
                _logger.LogError($"GetAllTaskDTOs: UserID {userId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<TaskDTO>();
            }

            return result;
        }

        public Task Get(int id)
        {
            Task? result;
            try
            {
                result = _db.Tasks
                    .Where(n => n.Id == id)
                    .Include(n => n.Group)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: Get: id {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Task();
            }
            if (result == null)
            {
                _logger.LogError($"TaskRepository: Get: id {id} Message: {EMPTY_VARIABLE}");
                return new Task();
            }
            _logger.LogDebug($"TaskRepository: Get: id {id} Message: {DATABASE_OK}");
            return result;
        }

        public List<Task> Get(List<int> ids)
        {
            List<Task> result;
            try
            {
                result = _db.Tasks
                    .Where(n => ids.Contains(n.Id))
                    .Include(n => n.Group)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Get ids: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Task>();
            }
            if (result == null)
            {
                _logger.LogError($"Get ids: Message: {EMPTY_VARIABLE}");
                return new List<Task>();
            }
            _logger.LogDebug($"Get ids: Message: {DATABASE_OK}");
            return result;
        }

        public IEnumerable<Task> GetTasksAssignedToGroup(int groupId)
        {
            List<Task> result;
            try
            {
                result = _db.Tasks
                    .Where(n => n.GroupId == groupId)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Get: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Task>();
            }
            if (result == null)
            {
                _logger.LogError($"Get: Message: {EMPTY_VARIABLE}");
                return new List<Task>();
            }
            _logger.LogDebug($"Get: Message: {DATABASE_OK}");
            return result;
        }

        public IEnumerable<int> GetTasksIdsAssignedToGroup(int groupId) => GetTasksAssignedToGroup(groupId).Select(n => n.Id);

        public bool DisconnectGroupFromTasks(int groupId)
        {
            List<Task> result = GetTasksAssignedToGroup(groupId).ToList();
            try
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    
                    result[i].GroupId = null;
                    _db.SaveChanges();
                    //Edit(result[i]);
                }
                Group group = _groupRepository.Get(groupId);
                group.Tasks = null;
                _groupRepository.Edit(group);
            }
            catch (Exception)
            {
                _logger.LogError($"DisconnectTasksFromGroup");
                return false;
            }
            _logger.LogDebug($"DisconnectTasksFromGroup: Message: {DATABASE_OK}");
            return true;
        }

        public bool DisconnectTaskFromGroup(int taskId)
        {
            Task result = new Task();
            try
            {
                result = Get(taskId);
                result.GroupId = null;
                Edit(result);
            }
            catch (Exception)
            {
                _logger.LogError($"DisconnectTaskFromGroup");
                return false;
            }
            _logger.LogDebug($"DisconnectTaskFromGroup: Message: {DATABASE_OK}");
            return true;
        }

        public bool DEBUG_ONLY_AssignUserIdToAllTables(string userId)
        {
            try
            {
                string currentAppUserId = userId;
                List<Task> tasks = _db.Tasks.ToList();
                tasks.ForEach(n => n.ApplicationUserId = currentAppUserId);
                List<Group> groups = _db.Groups.ToList();
                groups.ForEach(n => n.ApplicationUserId = currentAppUserId);
                List<Quote> quotes = _db.Quotes.ToList();
                quotes.ForEach(n => n.ApplicationUserId = currentAppUserId);
                List<Setting> settings = _db.Settings.ToList();
                settings.ForEach(n => n.ApplicationUserId = currentAppUserId);
                _db.SaveChanges();
                _logger.LogDebug($"TaskRepository: DEBUG_ONLY_TakeAllTasksAndAssignToCurrentUser: UserID {userId} Message: {DATABASE_OK}");

            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: DEBUG_ONLY_TakeAllTasksAndAssignToCurrentUser: UserID {userId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }

            return true;
        }
    }
}
