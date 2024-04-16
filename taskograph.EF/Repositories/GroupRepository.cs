using AutoMapper;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories.Infrastructure;
using Task = taskograph.Models.Tables.Task;
using Group = taskograph.Models.Tables.Group;
using static taskograph.Helpers.Messages;
using Microsoft.EntityFrameworkCore;
using taskograph.Models.Tables;
using taskograph.Web.Models.DTOs;

namespace taskograph.EF.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly TasksContext _db;
        private readonly ILogger<GroupRepository> _logger;
        private readonly IMapper _mapper;
        private ITaskRepository _taskRepository;

        public GroupRepository(TasksContext db, ILogger<GroupRepository> logger, IMapper mapper, ITaskRepository taskRepository)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public bool Add(Group group)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Group group)
        {
            throw new NotImplementedException();
        }

        public bool Edit(Group group)
        {
            throw new NotImplementedException();
        }

        public Group Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetAll(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> GetTasks(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
