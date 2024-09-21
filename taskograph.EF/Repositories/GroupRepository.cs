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

        public GroupRepository(TasksContext db, ILogger<GroupRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public bool Add(Group group)
        {
            bool result = true;
            try
            {
                group.Created = DateTime.Now;
                _db.Groups.Add(group);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = false;
            }
            return result;
        }

        public bool Delete(Group group)
        {
            bool result = true;
            try
            {
                group.Deleted = DateTime.Now;
                _db.Groups.Update(group);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = false;
            }
            return result;
        }

        public bool Edit(Group group)
        {
            bool result = true;
            try
            {
                group.LastUpdated = DateTime.Now;
                _db.Groups.Update(group);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = false;
            }
            return result;
        }

        public Group Get(int id)
        {
            Group? result;
            try
            {
                result = _db.Groups
                    .Include(n => n.Color)
                    .Where(n => n.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = new Group();
            }
            return result ?? new Group();
        }

        public IEnumerable<Group> GetAll(string userId)
        {
            List<Group> result;
            try
            {
                result = _db.Groups
                    .Include(n => n.Tasks)
                    .Where(n => n.ApplicationUserId == userId)
                    .Where(n => n.Deleted == null)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = new List<Group>();
            }
            return result ?? new List<Group>();
        }

        
    }
}