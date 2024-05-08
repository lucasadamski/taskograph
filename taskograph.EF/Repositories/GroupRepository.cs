using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using Task = taskograph.Models.Tables.Task;
using Group = taskograph.Models.Tables.Group;
using static taskograph.Helpers.Messages;
using Microsoft.EntityFrameworkCore;

namespace taskograph.EF.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<GroupRepository> _logger;
        private readonly IAppUserRepository _appUserRepository;

        public GroupRepository(TasksContext db, ILogger<GroupRepository> logger, IAppUserRepository appUserRepository)
        {
            _db = db;
            _logger = logger;
            _appUserRepository = appUserRepository;
        }

        public bool Add(Group group)
        {
            try
            {
                group.Created = DateTime.Now;
                _db.Groups.Add(group);
                _db.SaveChanges();
                _logger.LogDebug($"TaskRepository: Add {group.Name}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Add {group.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Delete(Group group)
        {
            try
            {
                group.Deleted = DateTime.Now;
                _db.Groups.Update(group);
                _db.SaveChanges();
                _logger.LogDebug($"Delete {group.Name}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Delete {group.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Edit(Group group)
        {
            try
            {
                group.LastUpdated = DateTime.Now;
                _db.Groups.Update(group);
                _db.SaveChanges();
                _logger.LogDebug($"Edit {group.Name}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"TEdit {group.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public Group Get(int id)
        {
            Group? result;
            try
            {
                result = _db.Groups
                    .Where(n => n.Id == id)
                    .Include(n => n.Color.Name)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Get: id {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Group();
            }
            if (result == null)
            {
                _logger.LogError($"Get: id {id} Message: {EMPTY_VARIABLE}");
                return new Group();
            }
            _logger.LogDebug($"Get: id {id} Message: {DATABASE_OK}");
            return result;
        }

        public IEnumerable<Group> GetAll(string userId)
        {
            List<Group> result;
            try
            {
                result = _db.Groups
                    .Include(n => n.Tasks)
                    .Where(n => n.AppUserId == _appUserRepository.GetId(userId))
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Get: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Group>();
            }
            if (result == null)
            {
                _logger.LogError($"Get: Message: {EMPTY_VARIABLE}");
                return new List<Group>();
            }
            _logger.LogDebug($"Get: Message: {DATABASE_OK}");
            return result;
        }

        public IEnumerable<Task> GetTasks(int groupId)
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
    }
}