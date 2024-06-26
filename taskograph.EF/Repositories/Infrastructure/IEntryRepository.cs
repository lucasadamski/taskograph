﻿using Group = taskograph.Models.Tables.Group;
using Task = taskograph.Models.Tables.Task;
using taskograph.Models.Tables;

namespace taskograph.EF.Repositories.Infrastructure
{
    public interface IEntryRepository
    {
        public bool Add(int taskId, long minutes, DateTime date);
        public bool Edit(Entry entry);
        public bool Delete(Entry entry);
        public Entry Get(int id);
        public IEnumerable<Entry> Get(string userId, DateTime from, DateTime to);
        public IEnumerable<Entry> GetByTask(int taskId, DateTime from, DateTime to);
        public IEnumerable<Entry> GetByGroup(int groupId, DateTime from, DateTime to);
        public IEnumerable<Entry> GetAll(string userId);
        public IEnumerable<Entry> GetAllByTask(int taskId, string userId);
        public IEnumerable<Entry> GetAllByGroup(int groupId, string userId);
        public Duration GetTotalDurationForTask(int taskId, DateTime date);
        public Duration GetTotalDurationForAllTasks(string userId, DateTime date);
        public Duration GetTotalDurationForTask(int taskId, DateTime dateFrom, DateTime dateTo);
    }
}
