using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using Microsoft.EntityFrameworkCore;

namespace taskograph.EF.Repositories
{
    public class PreciseTargetRepository : IPreciseTargetRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<PreciseTargetRepository> _logger;
        private readonly IMapper _mapper;

        public PreciseTargetRepository(TasksContext db, ILogger<PreciseTargetRepository> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public bool Add(PreciseTarget preciseTarget)
        {
            try
            {
                preciseTarget.Created = DateTime.Now;
                _db.PreciseTargets.Add(preciseTarget);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"PreciseTargetRepository: Add {preciseTarget.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Delete(PreciseTarget preciseTarget)
        {
            try
            {
                preciseTarget.Deleted = DateTime.Now;
                _db.PreciseTargets.Update(preciseTarget);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"PreciseTargetRepository: Delete {preciseTarget.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Edit(PreciseTarget preciseTarget)
        {
            try
            {
                preciseTarget.LastUpdated = DateTime.Now;
                _db.PreciseTargets.Update(preciseTarget);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"PreciseTargetRepository: Edit {preciseTarget.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public PreciseTarget Get(int id)
        {
            PreciseTarget? result;
            try
            {
                result = _db.PreciseTargets
                    .Where(n => n.Id == id)
                    .Include(n => n.Task)           //nullable
                    .Include(n => n.Task.Group)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"PreciseTargetRepository: Get: id {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new PreciseTarget();
            }
            if (result == null)
            {
                _logger.LogError($"PreciseTargetRepository: Get: id {id} Message: {EMPTY_VARIABLE}");
                return new PreciseTarget();
            }
            return result;
        }

       
        // Returns Targets with Date Due (not created!) from provided date range
        public IEnumerable<PreciseTarget> Get(string userId, DateTime from, DateTime to)
        {
            List<PreciseTarget> result;
            try
            {
                result = _db.PreciseTargets
                    .Include(n => n.Task.ApplicationUser)
                    .Where(n => n.Task.ApplicationUserId == userId)
                    .Where(n => (n.DateDue.Date >= from.Date) && (n.DateDue.Date <= to.Date))
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"PreciseTargetRepository: Get from {from} to {to} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<PreciseTarget>();
            }
            return result;
        }

        public IEnumerable<PreciseTarget> GetAll(string userId)
        {
            List<PreciseTarget> result;
            try
            {
                result = _db.PreciseTargets
                    .Include(n => n.Task.ApplicationUser)
                    .Where(n => n.Deleted == null)
                    .Where(n => n.Task.ApplicationUserId == userId)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"PreciseTargetRepository: GetAll from Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<PreciseTarget>();
            }
            return result;
        }
    }
}
