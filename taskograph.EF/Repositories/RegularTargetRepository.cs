using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using Microsoft.EntityFrameworkCore;
using taskograph.Models.StoredProcedures;
using taskograph.Models.DTOs;

namespace taskograph.EF.Repositories
{
    public class RegularTargetRepository : IRegularTargetRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<RegularTargetRepository> _logger;
        private readonly IMapper _mapper;

        public RegularTargetRepository(TasksContext db, ILogger<RegularTargetRepository> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public bool Add(RegularTarget regularTarget)
        {
            try
            {
                regularTarget.Created = DateTime.Now;
                _db.RegularTargets.Add(regularTarget);
                _db.SaveChanges();
                _logger.LogDebug($"RegularTargetRepository: Add TargetID: {regularTarget.Id}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"RegularTargetRepository: Add TargetID: {regularTarget.Id}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Delete(RegularTarget regularTarget)
        {
            try
            {
                regularTarget.Deleted = DateTime.Now;
                _db.Update(regularTarget);
                _db.SaveChanges();
                _logger.LogDebug($"RegularTargetRepository: Delete TargetID: {regularTarget.Id}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"RegularTargetRepository: Delete TargetID: {regularTarget.Id}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Edit(RegularTarget regularTarget)
        {
            try
            {
                regularTarget.LastUpdated = DateTime.Now;
                _db.RegularTargets.Update(regularTarget);
                _db.SaveChanges();
                _logger.LogDebug($"RegularTargetRepository: Edit TargetID: {regularTarget.Id}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"RegularTargetRepository: Edit TargetID: {regularTarget.Id}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public RegularTarget Get(int id)
        {
            RegularTarget? result;
            try
            {
                result = _db.RegularTargets
                    .Where(n => n.Id == id)
                    .Include(n => n.Task)           //nullable
                    .Include(n => n.Task.Group)
                    .Include(n => n.TargetDuration)
                    .Include(n => n.PerTimeframeDuration)
                    .Where(n => n.Deleted == null)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"RegularTargetRepository: Get: id {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new RegularTarget();
            }
            if (result == null)
            {
                _logger.LogError($"RegularTargetRepository: Get: id {id} Message: {EMPTY_VARIABLE}");
                return new RegularTarget();
            }
            _logger.LogDebug($"RegularTargetRepository: Get: id {id} Message: {DATABASE_OK}");
            return result;
        }

        public IEnumerable<RegularTargetDTO> Get(string userId, DateTime? from = null, DateTime? to = null)
        {
            List<RegularTargetSP> spOutput;
            List<RegularTargetDTO> result;
            string query = (from == null && to == null) ? ($"exec spGetAllRegularTargets '{userId}';") : ($"exec spGetRegularTargets '{userId}', '{from}', '{to}';") ;
            try
            {
                spOutput = _db.Database.SqlQueryRaw<RegularTargetSP>(query).ToList();
                result = spOutput
                    .Where(n => n.Deleted == null)
                    .Select(n => new RegularTargetDTO()
                {
                    Id = n.Id,
                    TaskName = n.TaskName,
                    TargetDuration = new Duration() { Minutes = n.TargetDuration},
                    PerTimeframeDuration = new Duration() { Minutes = n.PerTimeframeDuration},
                    Created = n.Created,
                    LastUpdated = n.LastUpdated,
                    Deleted = n.Deleted
                }).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"RegularTargetRepository: Get from {(from ?? new DateTime())} to {(to ?? new DateTime())} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<RegularTargetDTO>();
            }
            _logger.LogDebug($"RegularTargetRepository: Get Get from {(from ?? new DateTime())} to {(to ?? new DateTime())} Message: {DATABASE_OK}");
            return result;
        }
    }
}
