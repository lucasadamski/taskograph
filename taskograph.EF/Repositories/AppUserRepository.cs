using Microsoft.Extensions.Logging;
using Task = taskograph.Models.Tables.Task;
using Group = taskograph.Models.Tables.Group;
using Color = taskograph.Models.Tables.Color;
using static taskograph.Helpers.Messages;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace taskograph.EF.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<AppUserRepository> _logger;

        public AppUserRepository(TasksContext db, ILogger<AppUserRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public bool Add(string userId)
        {
            AppUser appUser = new AppUser() { UserId = userId };
            try
            {
                _db.AppUsers.Add(appUser);
                _db.SaveChanges();
                _logger.LogDebug($"Add {userId}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Add {userId}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public AppUser Get(int id)
        {
            AppUser? result;
            try
            {
                result = _db.AppUsers
                    .Where(n => n.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Get: id {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new AppUser();
            }
            if (result == null)
            {
                _logger.LogError($"Get: id {id} Message: {EMPTY_VARIABLE}");
                return new AppUser();
            }
            _logger.LogDebug($"Get: id {id} Message: {DATABASE_OK}");
            return result;
        }

        public int GetId(string userId)
        {
            int result;
            try
            {
                result = _db.AppUsers
                    .Where(n => n.UserId == userId)
                    .Select(n => n.Id)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Get: id {userId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return -1;
            }
            _logger.LogDebug($"Get: id {userId} Message: {DATABASE_OK}");
            return result;
        }
    }
}
