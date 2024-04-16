using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using Group = taskograph.Models.Tables.Group;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;

namespace taskograph.EF.Repositories
{
    public class DurationRepository : IDurationRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<DurationRepository> _logger;
        private readonly IMapper _mapper;

        public DurationRepository(TasksContext db, ILogger<DurationRepository> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<Duration> GetAll()
        {
            List<Duration> result;
            try
            {
                result = _db.Durations.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"DurationRepository: GetAll Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Duration>();
            }
            _logger.LogDebug($"DurationRepository: GetAll Message: {DATABASE_OK}");
            return result;
        }

        public IEnumerable<Duration> GetFirst(int amount)
        {
            List<Duration> result;
            try
            {
                result = _db.Durations
                    .Take(amount)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"DurationRepository: GetAll Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Duration>();
            }
            _logger.LogDebug($"DurationRepository: GetAll Message: {DATABASE_OK}");
            return result;
        }
        public Duration Get(int id)
        {
            Duration result;
            try
            {
                result = _db.Durations.Where(n => n.Id == id).First();
            }
            catch (Exception e)
            {
                _logger.LogError($"Get Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Duration();
            }
            _logger.LogDebug($"Get Message: {DATABASE_OK}");
            return result;
        }
        public Duration Add(Duration duration)
        {
            Duration? existingDuration = GetExistingDuration(duration.Minutes);
            if (existingDuration != null)
            {
                _logger.LogDebug($"Add: Duration already exists Id {existingDuration.Id} Message: {DATABASE_OK}");
                return existingDuration;
            }
            else
            {                
                _db.Durations.Add(duration);
                _db.SaveChanges();
                _logger.LogDebug($"Add: Added new Duration Id {duration.Id} Message: {DATABASE_OK}");
                return duration;
            }
        }
        public Duration? GetExistingDuration(long minutes)
        {
            Duration? result;
            try
            {
                result = _db.Durations
                    .Where(n => n.Minutes == minutes)
                    .First();
            }
            catch (Exception e)
            {
                return null;
            }
            return result;
        }
    }
}
