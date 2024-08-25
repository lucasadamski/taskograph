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
    public class TaskRepositoryTests
    {
        private readonly TasksContext _db;
        private readonly ILogger<TaskRepository> _logger;
        private readonly IMapper _mapper;
        private IEntryRepository _entryRepository;
        private IGroupRepository _groupRepository;
        private string _userId = "testUserId";

        public TaskRepositoryTests()
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
            dbContext.Database.EnsureCreated();

            //filling with seed data
            if (await dbContext.Tasks.CountAsync() < 0)
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




    }
}