﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using Microsoft.EntityFrameworkCore;
using taskograph.Models.Tables;
using taskograph.Web.Models.DTOs;

namespace taskograph.EF.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<TaskRepository> _logger;
        private readonly IMapper _mapper;
        private IEntryRepository _entryRepository;

        public TaskRepository(TasksContext db, ILogger<TaskRepository> logger, IMapper mapper, IEntryRepository entryRepository)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _entryRepository = entryRepository;
        }

        public bool Add(Task task)
        {
            try
            {
                task.Created = DateTime.Now;
                _db.Tasks.Add(task);
                _db.SaveChanges();
                _logger.LogDebug($"TaskRepository: Add {task.Name}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: Add {task.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Delete(Task task)
        {
            try
            {
                task.Deleted = DateTime.Now;
                _db.Tasks.Update(task);
                _db.SaveChanges();
                _logger.LogDebug($"TaskRepository: Delete {task.Name}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: Delete {task.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public bool Edit(Task task)
        {
            try
            {
                task.LastUpdated = DateTime.Now;
                _db.Tasks.Update(task);
                _db.SaveChanges();
                _logger.LogDebug($"TaskRepository: Edit {task.Name}: Message: {DATABASE_OK}");
            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: Edit {task.Name}: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }
            return true;
        }

        public IEnumerable<Task> GetAll(string userId)
        {
            IEnumerable<Task> result;
            try
            {
                result = _db.Tasks.Include(n => n.Group) //TODO add UserId column
                    .Include(n => n.Color)
                    .Include(n => n.AppUser)
                    .Where(n => n.Deleted == null)
                    .Where(n => n.AppUser.UserId == userId)
                    .ToList();
                _logger.LogDebug($"TaskRepository: GetAllTasks: UserID {userId} Message: {DATABASE_OK}");

            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: GetAllTasks: UserID {userId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Task>();
            }

            return result;
        }
        public IEnumerable<Task> GetAllUnassigned(string userId)
        {
            IEnumerable<Task> result;
            try
            {
                result = _db.Tasks.Include(n => n.Group) //TODO add UserId column
                    .Include(n => n.Color)
                    .Include(n => n.AppUser)
                    .Where(n => n.GroupId == null)
                    .Where(n => n.Deleted == null)
                    .Where(n => n.AppUser.UserId == userId)
                    .ToList();
                _logger.LogDebug($"TaskRepository: GetAllTasks: UserID {userId} Message: {DATABASE_OK}");

            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: GetAllTasks: UserID {userId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Task>();
            }

            return result;
        }

        public IEnumerable<TaskDTO> GetAllTaskDTOs(string userId)
        {
            IEnumerable<TaskDTO> result;
            
            try
            {
                result = GetAll(userId)
                    .Select(n => new TaskDTO()
                    {
                        Id = n.Id,
                        Name = n.Name,
                        Group = n.Group?.Name ?? NULL_VALUE,
                        Color = n.Color?.Name ?? NULL_VALUE,
                        TotalDurationToday = (_entryRepository.GetTotalDurationForTask(n.Id, DateTime.Now))
                    })
                 .ToList();
                _logger.LogDebug($"GetAllTaskDTOs: UserID {userId} Message: {DATABASE_OK}");

            }
            catch (Exception e)
            {
                _logger.LogError($"GetAllTaskDTOs: UserID {userId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<TaskDTO>();
            }

            return result;
        }

        public Task Get(int id)
        {
            Task? result;
            try
            {
                result = _db.Tasks
                    .Where(n => n.Id == id)
                    .Include(n => n.Group)
                    .Include(n => n.Color)
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: Get: id {id} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new Task();
            }
            if (result == null)
            {
                _logger.LogError($"TaskRepository: Get: id {id} Message: {EMPTY_VARIABLE}");
                return new Task();
            }
            _logger.LogDebug($"TaskRepository: Get: id {id} Message: {DATABASE_OK}");
            return result;
        }

        public List<Task> Get(List<int> ids)
        {
            List<Task> result;
            try
            {
                result = _db.Tasks
                    .Where(n => ids.Contains(n.Id))
                    .Include(n => n.Group)
                    .Include(n => n.Color)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError($"Get ids: Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return new List<Task>();
            }
            if (result == null)
            {
                _logger.LogError($"Get ids: Message: {EMPTY_VARIABLE}");
                return new List<Task>();
            }
            _logger.LogDebug($"Get ids: Message: {DATABASE_OK}");
            return result;
        }

        public bool DEBUG_ONLY_AssignUserIdToAllTables(string userId)
        {
            try
            {
                int currentAppUserId = _db.AppUsers.Where(n => n.UserId == userId).Select(n => n.Id).FirstOrDefault();
                List<Task> tasks = _db.Tasks.ToList();
                tasks.ForEach(n => n.AppUserId = currentAppUserId);
                List<Group> groups = _db.Groups.ToList();
                groups.ForEach(n => n.AppUserId = currentAppUserId);
                List<Quote> quotes = _db.Quotes.ToList();
                quotes.ForEach(n => n.AppUserId = currentAppUserId);
                List<Setting> settings = _db.Settings.ToList();
                settings.ForEach(n => n.AppUserId = currentAppUserId);
                _db.SaveChanges();
                _logger.LogDebug($"TaskRepository: DEBUG_ONLY_TakeAllTasksAndAssignToCurrentUser: UserID {userId} Message: {DATABASE_OK}");

            }
            catch (Exception e)
            {
                _logger.LogError($"TaskRepository: DEBUG_ONLY_TakeAllTasksAndAssignToCurrentUser: UserID {userId} Message: {DATABASE_ERROR_CONNECTION} Exception: {e.Message}");
                return false;
            }

            return true;
        }
    }
}
