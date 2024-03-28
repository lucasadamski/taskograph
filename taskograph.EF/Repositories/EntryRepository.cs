using Group = taskograph.Models.Tables.Group;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using taskograph.Models.Tables;
using taskograph.EF.Repositories.Infrastructure;
using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace taskograph.EF.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<EntryRepository> _logger;
        private readonly IMapper _mapper;

        public EntryRepository(TasksContext db, ILogger<EntryRepository> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public bool Add(Entry entry)
        {
            //if entry for same task and day exists, then add current duration to existing entry
            Entry? alreadyAddedToday = GetEntryFromSameTaskAndDay(entry);
            if (alreadyAddedToday != null)
            {
                EditAddDuration(alreadyAddedToday, entry.Duration);
                Delete(entry);
                _logger.LogDebug($"EntryRepository: Add: EntryId {entry.Id} merged with Already Existing EntryId: {alreadyAddedToday.Id} Message: {DATABASE_OK}");
            }
            else //if not, create new entry
            {               
                try
                {
                    _db.Entries.Add(entry);
                    _db.SaveChanges();
                    _logger.LogDebug($"EntryRepository: Add: EntryId {entry.Id} Message: {DATABASE_OK}");
                }
                catch (Exception e)
                {
                    _logger.LogError($"EntryRepository: Add: EntryId {entry.Id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                    return false;
                }
            }
            return true;
        }

        private Entry? GetEntryFromSameTaskAndDay(Entry entry)
        {
            Entry result = new Entry();
            try
            {
                result = _db.Entries
                .Where(n => n.Date.Created.Date == entry.Date.Created.Date)
                .Where(n => n.TaskId == entry.TaskId)
                .First();
            }
            catch (Exception e)
            {
                _logger.LogDebug($"EntryRepository: GetEntryFromSameTaskAndDay: EntryId {entry.Id}: Message: {DATABASE_ERROR_CONNECTION}");
                return null;
            }
            return result;
        }

        public bool Delete(Entry entry)
        {
            try
            {
                _db.Entries.Remove(entry);
                _db.SaveChanges();
                _logger.LogDebug($"EntryRepository: Delete EntryId: {entry.Id} Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: Delete EntryId: {entry.Id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Edit(Entry entry)
        {
            try
            {
                _db.Entries.Update(entry);
                _db.SaveChanges();
                _logger.LogDebug($"EntryRepository: Delete EntryId: {entry.Id} Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: Delete EntryId: {entry.Id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }
        public bool EditAddDuration(Entry entry, Duration duration)
        {
            entry.Duration.End += duration.End.TimeOfDay; //Warining, works only for values up to 24 hours
            //TODO write code for all times, more than 24 hours
            Edit(entry);
            return true;
        }

        public Entry Get(int id)
        {
            Entry? result;
            try
            {
                result = _db.Entries
                    .Where(n => n.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: Get EntryId: {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Entry();
            }
            if (result == null)
            {
                _logger.LogError($"EntryRepository: Get EntryId: {id} Message: {EMPTY_VARIABLE}");
                return new Entry();
            }
            _logger.LogDebug($"EntryRepository: Get EntryId: {id} Message: {DATABASE_OK}");
            return result;
        }

        public IEnumerable<Entry> Get(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entry> GetAll(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entry> GetAllByGroup(int groupId, string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entry> GetAllByTask(int taskId, string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entry> GetByGroup(int groupId, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entry> GetByTask(int taskId, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

    }
}
