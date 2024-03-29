using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using Microsoft.EntityFrameworkCore;

namespace taskograph.EF.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<TaskRepository> _logger;
        private readonly IMapper _mapper;

        public TaskRepository(TasksContext db, ILogger<TaskRepository> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public bool Add(Task task)
        {
            try
            {
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
                _db.Tasks.Remove(task);
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
                    .Include(n => n.Color)
                    .Where(n => n.UserId == userId)
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

        public Task Get(int id)
        {
            Task? result;
            try
            {
                result = _db.Tasks
                    .Where(n => n.Id == id)
                    .Include(n => n.Group)
                    .Include(n => n.Color)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: GetTask: id {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Task();
            }
            if (result == null)
            {
                _logger.LogError($"TaskRepository: GetTask: id {id} Message: {EMPTY_VARIABLE}");
                return new Task();
            }
            _logger.LogDebug($"TaskRepository: GetTask: id {id} Message: {DATABASE_OK}");
            return result;
        }

        public bool DEBUG_ONLY_TakeAllTasksAndAssignToCurrentUser(string userId)
        {
            try
            {
                List<Task> result = _db.Tasks.ToList();
                result.ForEach(n => n.UserId = userId);
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
