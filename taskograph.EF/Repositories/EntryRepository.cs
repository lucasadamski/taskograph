﻿using Group = taskograph.Models.Tables.Group;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using taskograph.Models.Tables;
using taskograph.EF.Repositories.Infrastructure;
using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace taskograph.EF.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<EntryRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IDurationRepository _durationRepository;
        public EntryRepository(TasksContext db, ILogger<EntryRepository> logger, IMapper mapper, 
            IDurationRepository durationRepository)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _durationRepository = durationRepository;
        }

        private bool Add(Entry entry)
        {                         
            try
            {
                entry.Created = DateTime.Now;
                _db.Entries.Add(entry);
                _db.SaveChanges();
               
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: Add: EntryId {entry.Id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }
        public bool Add(int taskId, long minutes, DateTime date)
        {
            try
            {
                Entry? existingEntry = GetExistingEntry(taskId, date);
                if (existingEntry != null)
                {
                    Duration? existingDuration = _durationRepository.GetExistingDuration(existingEntry.Duration.Minutes + minutes);
                    if (existingDuration == null)
                    {
                        existingDuration = _durationRepository.Add(new Duration() { Minutes = existingEntry.Duration.Minutes + minutes });
                    }
                    existingEntry.DurationId = existingDuration.Id;
                    Edit(existingEntry);
                }
                else
                {
                    Entry newEntry = new Entry();
                    Duration? existingDuration = _durationRepository.GetExistingDuration(minutes);
                    if (existingDuration == null)
                    {
                        existingDuration = _durationRepository.Add(new Duration() { Minutes = minutes });
                    }
                    newEntry.DurationId = existingDuration.Id;
                    newEntry.TaskId = taskId;
                    Add(newEntry);
                }
                return true;

            }
            catch (Exception e)
            {
                _logger.LogError($"Add: TaskId {taskId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
        }

        public bool Delete(Entry entry)
        {
            try
            {
                entry.Deleted = DateTime.Now;
                _db.Entries.Update(entry);
                _db.SaveChanges();
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
                entry.LastUpdated = DateTime.Now;
                _db.Entries.Update(entry);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: Delete EntryId: {entry.Id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
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
            return result;
        }

        public IEnumerable<Entry> Get(string userId, DateTime from, DateTime to)
        {
            List<Entry> result;
            try
            {
                result = _db.Entries
                    .Include(n => n.Task.ApplicationUser)
                    .Where(n => n.Task.ApplicationUserId == userId)
                    .Where(n => (n.Created.Date >= from.Date) && (n.Created.Date <= to.Date))
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: Get from {from} to {to} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Entry>();
            }
            return result;
        }

        public IEnumerable<Entry> GetAll(string userId)
        {
            List<Entry> result;
            try
            {
                result = _db.Entries
                    .Include(n => n.Task.ApplicationUser)
                    .Where(n => n.Task.ApplicationUserId == userId)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: GetAll Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Entry>();
            }
            return result;
        }

        public IEnumerable<Entry> GetAllByGroup(int groupId, string userId)
        {
            List<Entry> result;
            try
            {
                result = _db.Entries
                    .Include(n => n.Task.ApplicationUser)
                    .Where(n => n.Task.ApplicationUserId == userId)
                    .Where(n => n.Task.GroupId == groupId)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: GetAllByGroup groupId:{groupId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Entry>();
            }
            return result;
        }

        public IEnumerable<Entry> GetAllByTask(int taskId, string userId)
        {
            List<Entry> result;
            try
            {
                result = _db.Entries
                   .Include(n => n.Task.ApplicationUser)
                    .Where(n => n.Task.ApplicationUserId == userId)
                    .Where(n => n.TaskId == taskId)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: GetAllByTask taskId:{taskId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Entry>();
            }
            return result;
        }

        public IEnumerable<Entry> GetByGroup(int groupId, DateTime from, DateTime to)
        {
            List<Entry> result;
            try
            {
                result = _db.Entries
                    .Include(n => n.Task)
                    .Where(n => n.Task.GroupId == groupId)
                    .Where(n => (n.Created.Date >= from.Date) && (n.Created.Date <= to.Date))
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: GetByGroup from {from} to {to} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Entry>();
            }
            return result;
        }

        public IEnumerable<Entry> GetByTask(int taskId, DateTime from, DateTime to)
        {
            List<Entry> result;
            try
            {
                result = _db.Entries
                    .Where(n => n.TaskId == taskId)
                    .Where(n => (n.Created.Date >= from.Date) && (n.Created.Date <= to.Date))
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: GetByTask from {from} to {to} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Entry>();
            }
            return result;
        }

        public Duration GetTotalDurationForTask(int taskId, DateTime date)
        {
            List<Duration> durationsList = new List<Duration>();
            Duration durationTotal = new Duration();
            try
            {
                durationsList = _db.Entries
                    .Include(n => n.Duration)
                    .Where(n => n.TaskId == taskId)
                    .Where(n => n.Created.Date == date.Date)
                    .Select(n => n.Duration)
                    .ToList();

                durationTotal = durationsList.Aggregate((a,b) => a + b);
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: GetTotalDurationForTask taskId {taskId} date {date} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Duration();
            }
            return durationTotal;
        }
        public Duration GetTotalDurationForAllTasks(string userId, DateTime date)
        {
            List<Duration> durationsList = new List<Duration>();
            Duration durationTotal = new Duration();
            try
            {
                durationsList = _db.Entries
                    .Include(n => n.Duration)
                    .Include(n => n.Task.ApplicationUser)
                    .Where(n => n.Task.ApplicationUserId == userId)
                    .Where(n => n.Created.Date == date.Date)
                    .Select(n => n.Duration)
                    .ToList();

                durationTotal = durationsList.Aggregate((a, b) => a + b);
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: GetTotalDurationForAllTasks date {date} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Duration();
            }
            return durationTotal;
        }
        public Duration GetTotalDurationForTask(int taskId, DateTime dateFrom, DateTime dateTo)
        {
            Duration result;
            try
            {
                result = _db.Entries
                    .Include(n => n.Duration)
                    .Where(n => n.TaskId == taskId) 
                    .Where(n => (n.Created.Date >= dateFrom.Date) && (n.Created.Date <= dateTo.Date))
                    .Select(n => n.Duration)
                    .Aggregate((a, b) => a + b);
            }
            catch (Exception e)
            {
                _logger.LogError($"EntryRepository: GetTotalDurationForTask taskId {taskId} date {dateFrom} - {dateTo} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Duration();
            }
            return result;
        }

        private Entry? GetExistingEntry(int taskId, DateTime day)
        {
            Entry? result;
            try
            {
                result = _db.Entries
                    .Where(n => n.TaskId == taskId)
                    .Where(n => (n.LastUpdated == null ? (n.Created.Date == day.Date) : (n.LastUpdated.Value.Date == day.Date)))
                    .First();
            }
            catch (Exception e)
            {
                return null;
            }
            result.Duration = _durationRepository.Get(result.DurationId);
            return result;
        }

    }
}
