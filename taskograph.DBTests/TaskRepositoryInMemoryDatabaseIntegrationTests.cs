using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Models.DTOs;
using Task = taskograph.Models.Tables.Task;

namespace taskograph.DBTests
{
    public class TaskRepositoryInMemoryDatabaseIntegrationTests
    {
        private readonly TasksContext _db;
        private readonly ILogger<TaskRepository> _logger;
        private readonly IMapper _mapper;
        private IEntryRepository _entryRepository;
        private IGroupRepository _groupRepository;
        private string _userId = "testUserId";

        public TaskRepositoryInMemoryDatabaseIntegrationTests()
        {
            _logger = A.Fake<ILogger<TaskRepository>>();
            _mapper = A.Fake<IMapper>();
            _entryRepository = A.Fake<IEntryRepository>();
            _groupRepository = A.Fake<IGroupRepository>();
        }

        private async Task<TasksContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TasksContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //in memory nuget necessary
                .Options;
            //creating DBcontext from mock
            var dbContext = new TasksContext(options);
            dbContext.Database.EnsureDeleted();

            //filling with seed data
            if (await dbContext.Tasks.CountAsync() == 0)
            {
                dbContext.Tasks.Add(new Task { Id = 1, Name = "Running", GroupId = 4, Created = DateTime.Now, ApplicationUserId = _userId });
                dbContext.Tasks.Add(new Task { Id = 2, Name = "Cooking", GroupId = 4, Created = DateTime.Now, ApplicationUserId = _userId });
                dbContext.Tasks.Add(new Task { Id = 3, Name = "Reading", GroupId = 4, Created = DateTime.Now, ApplicationUserId = _userId });
                await dbContext.SaveChangesAsync();
            }
            return dbContext;
        }

        private void ResetDbContext(TasksContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
        }


        [Fact]
        public async void TasksRepository_Add_ReturnsTrue()
        {
            //Arrange
            var task = new Task { Name = "Testing", GroupId = 4, Created = DateTime.Now, ApplicationUserId = _userId };

            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.Add(task);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void TaskRepository_Add_ReturnsFalse()
        {
            //Arrange
            var task = new Task { Name = null, GroupId = 4, Created = DateTime.Now, ApplicationUserId = _userId };
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.Add(task);
            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void TaskRepository_Delete_ReturnsTrue()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            var task = taskRepository.Get(1);
            //Act
            var result = taskRepository.Delete(task);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void TaskRepository_Delete_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            Task task = null;
            //Act
            var result = taskRepository.Delete(task);
            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void TaskRepository_Edit_ReturnsTrue()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            var task = taskRepository.Get(1);
            task.Name = "Edited Name";
            //Act
            var result = taskRepository.Edit(task);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void TaskRepository_Edit_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            Task task = null;
            //Act
            var result = taskRepository.Edit(task);
            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void TaskRepository_GetAll_ReturnsThreeElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.GetAll(_userId);
            //Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public async void TaskRepository_GetAll_ReturnsZeroElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.GetAll("someRandomUserId!@%!@#$");
            //Assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public async void TaskRepository_GetAll_TakesNull_ReturnsZeroElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.GetAll(null);
            //Assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public async void TaskRepository_GetAllUnassigned_ReturnsOneElement()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            var task = new Task { Name = "NoGroupAssignedTask", GroupId = null, Created = DateTime.Now, ApplicationUserId = _userId };
            taskRepository.Add(task);
            //Act
            var result = taskRepository.GetAllUnassigned(_userId);
            //Assert
            result.Should().HaveCount(1);
        }

        [Fact]
        public async void TaskRepository_GetAllUnassigned_ReturnsZeroElement()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.GetAllUnassigned(_userId);
            //Assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public async void TaskRepository_GetAllTaskDTOs_ReturnsThreeElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.GetAllTaskDTOs(_userId);
            //Assert
            result.Should().HaveCount(3);
            result.ElementAt(0).Should().BeOfType(typeof(TaskDTO));
        }

        [Fact]
        public async void TaskRepository_Get_ReturnsValidObjectId1()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.Get(1);
            //Assert
            result.Should().BeOfType(typeof(Task));
            result.Id.Should().Be(1);
        }

        [Fact]
        public async void TaskRepository_Get_TakesNonExistingId_ReturnsEmptyElement()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.Get(3421);
            //Assert
            result.Should().BeOfType(typeof(Task));
            result?.Id.Should().Be(0);
        }

        [Fact]
        public async void TaskRepository_Get_TakesListOfThreeIds_ReturnsListWithThreeElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            List<int> ids = new List<int>() { 1, 2, 3 };
            //Act
            var result = taskRepository.Get(ids);
            //Assert
            result.Should().BeOfType(typeof(List<Task>));
            result.Should().HaveCount(3);
        }

        [Fact]
        public async void TaskRepository_Get_TakesNull_ReturnsEmptyList()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.Get(null);
            //Assert
            result.Should().BeOfType(typeof(List<Task>));
            result.Should().HaveCount(0);
        }

        [Fact]
        public async void TaskRepository_Get_TakesTooManyIds_ReturnsListWithThreeElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            List<int> ids = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 123 };
            //Act
            var result = taskRepository.Get(ids);
            //Assert
            result.Should().BeOfType(typeof(List<Task>));
            result.Should().HaveCount(3);
        }

        [Fact]
        public async void TaskRepository_Get_TakesTwoIds_ReturnsListWithTwoElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            List<int> ids = new List<int>() { 1, 2 };
            //Act
            var result = taskRepository.Get(ids);
            //Assert
            result.Should().BeOfType(typeof(List<Task>));
            result.Should().HaveCount(2);
        }

        [Fact]
        public async void TaskRepository_GetTasksAssignedToGroup_TakesGroupId_ReturnsListWithThreeElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.GetTasksAssignedToGroup(4);
            //Assert
            result.Should().BeOfType(typeof(List<Task>));
            result.Should().HaveCount(3);
        }

        [Fact]
        public async void TaskRepository_GetTasksAssignedToGroup_TakesGroupId_ReturnsListWithOneElement()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            var task = new Task { Name = "Testing", GroupId = 34, Created = DateTime.Now, ApplicationUserId = _userId };
            //Act
            taskRepository.Add(task);
            var result = taskRepository.GetTasksAssignedToGroup(34);
            //Assert
            result.Should().BeOfType(typeof(List<Task>));
            result.Should().HaveCount(1);
        }

        [Fact]
        public async void TaskRepository_GetTasksAssignedToGroup_TakesGroupId_ReturnsEmpty()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.GetTasksAssignedToGroup(3314);
            //Assert
            result.Should().BeOfType(typeof(List<Task>));
            result.Should().HaveCount(0);
        }

        [Fact]
        public async void TaskRepository_GetTasksIdsAssignedToGroup_TakesGroupId_ReturnsListWithThreeElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.GetTasksIdsAssignedToGroup(4).ToList();
            //Assert
            result.Should().BeOfType(typeof(List<int>));
            result.Should().HaveCount(3);
        }

        [Fact]
        public async void TaskRepository_GetTasksIdsAssignedToGroup_TakesGroupId_ReturnsEmptyList()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.GetTasksIdsAssignedToGroup(432).ToList();
            //Assert
            result.Should().BeOfType(typeof(List<int>));
            result.Should().HaveCount(0);
        }

        [Fact]
        public async void TaskRepository_DisconnectGroupFromTasks_TakesGroupId_ReturnsTrue()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.DisconnectGroupFromTasks(4);
            var allTasks = taskRepository.GetAll(_userId);
            //Assert
            result.Should().BeTrue();
            foreach (var element in allTasks)
            {
                element.GroupId?.Should().Be(null);
            }
        }

        [Fact]
        public async void TaskRepository_DisconnectGroupFromTasks_TakesGroupId_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.DisconnectGroupFromTasks(434);
            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void TaskRepository_DisconnectGroupFromTasks_TakesNegativeGroupId_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.DisconnectGroupFromTasks(-434);
            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void TaskRepository_DisconnectTaskFromGroup_TakesTaskId_ReturnsTrue()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.DisconnectTaskFromGroup(1);
            var allElements = taskRepository.GetTasksAssignedToGroup(4);
            var disconnectedTask = taskRepository.Get(1);
            //Assert
            result.Should().BeTrue();
            allElements.Should().HaveCount(2);
            disconnectedTask.GroupId?.Should().Be(null);  
        }





    }
}