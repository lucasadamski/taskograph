using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
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
                dbContext.Tasks.Add(new Task { Id = 1, Name = "Running", GroupId = 4, Created = DateTime.Now, ApplicationUserId = "none" });
                dbContext.Tasks.Add(new Task { Id = 2, Name = "Cooking", GroupId = 4, Created = DateTime.Now, ApplicationUserId = "none" });
                dbContext.Tasks.Add(new Task { Id = 3, Name = "Reading", GroupId = 4, Created = DateTime.Now, ApplicationUserId = "none" });
                await dbContext.SaveChangesAsync();
            }
            return dbContext;
        }


        [Fact]
        public async void TasksRepository_Add_ReturnsTrue()
        {
            //Arrange
            var task = new Task {Name = "Testing", GroupId = 4, Created = DateTime.Now, ApplicationUserId = "none" };
            
            var dbContext = await GetDbContext();
            var taskRepository = new TaskRepository(dbContext, _logger, _mapper, _entryRepository, _groupRepository);
            //Act
            var result = taskRepository.Add(task);
            //Assert
            result.Should().BeTrue();
        }
    }
}