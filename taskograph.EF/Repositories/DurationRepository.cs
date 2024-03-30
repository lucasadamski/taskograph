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
    }
}
