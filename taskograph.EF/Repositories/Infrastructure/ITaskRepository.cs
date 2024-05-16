using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taskograph.Web.Models.DTOs;
using Task = taskograph.Models.Tables.Task;

namespace taskograph.EF.Repositories.Infrastructure
{
    public interface ITaskRepository
    {
        bool Add(Task task);
        bool Edit(Task task);
        bool Delete(Task task);

        public IEnumerable<Task> GetAll(string userId);
        public IEnumerable<Task> GetAllUnassigned(string userId);
        public Task Get(int id);
        public List<Task> Get(List<int> ids);
        public IEnumerable<TaskDTO> GetAllTaskDTOs(string userId);
        public IEnumerable<Task> GetTasksAssignedToGroup(int groupId);
        public IEnumerable<int> GetTasksIdsAssignedToGroup(int groupId);
        public bool DisconnectTasksFromGroup(int groupId);
        public bool DisconnectTaskFromGroup(int taskId);

        //Debug only
        public bool DEBUG_ONLY_AssignUserIdToAllTables(string userId);



    }
}
