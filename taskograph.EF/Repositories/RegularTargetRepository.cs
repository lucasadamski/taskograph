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
using Microsoft.Identity.Client;

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
            bool result = true;
            try
            {
                if (regularTarget.RegularTimeIntervalToAchieveTarget == 0 || regularTarget.TimeDedicatedToPerformTarget ==  0) 
                        throw new Exception("One of mandatory fields are empty");
                regularTarget.Created = DateTime.Now;
                _db.RegularTargets.Add(regularTarget);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = false;
            }
            return result;
        }

        public bool Delete(RegularTarget regularTarget)
        {
            bool result = true;
            try
            {
                if (!_db.RegularTargets.Contains(regularTarget)) throw new Exception("Can't delete this object because it does not exist in DB.");
                regularTarget.Deleted = DateTime.Now;
                _db.Update(regularTarget);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result =false;
            }
            return result;
        }

        public bool Edit(RegularTarget regularTarget)
        {
            bool result = true;
            try
            {
                if (!_db.RegularTargets.Contains(regularTarget)) throw new Exception("Can't delete this object because it does not exist in DB.");
                regularTarget.LastUpdated = DateTime.Now;
                _db.RegularTargets.Update(regularTarget);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = false;
            }
            return result;
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
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                result = null;
            }
            return result ?? new RegularTarget();
        }

        //Not tested in integration tests. InMemoryDB can't test raw sql queries.
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
                    TargetDuration =  n.TargetDuration,
                    PerTimeframeDuration = n.PerTimeframeDuration,
                    Created = n.Created,
                    LastUpdated = n.LastUpdated,
                    Deleted = n.Deleted
                }).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message} StackTrace: {e.StackTrace}");
                return new List<RegularTargetDTO>();
            }
            return result ?? new List<RegularTargetDTO>();
        }
    }
}
