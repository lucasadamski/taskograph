using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using taskograph.EF.DataAccess;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Models.DTOs;
using Task = taskograph.Models.Tables.Task;
using taskograph.Models.Tables;
using taskograph.Models;

namespace taskograph.DBTests
{
    public class EntryRepositoryUnitTests
    {
        private readonly TasksContext _db;
        private readonly ILogger<EntryRepository> _logger;
        private readonly IMapper _mapper;

        //values for dummy entries
        private int _entryId_1 = 1;
        private int _entryId_2 = 2;
        private int _entryId_3 = 3;
        private int _taskId_1 = 1;
        private int _taskId_2 = 2;
        private int _taskId_3 = 3;
        private long _duration = 100;
        private Duration _durationObject = new Duration(100L);
        private DateTime _created = new DateTime(1999, 1, 1);
        private DateTime? _lastUpdated = new DateTime(2020, 1, 1);
        private string _userId = "testUserId";

        public EntryRepositoryUnitTests()
        {
            _logger = A.Fake<ILogger<EntryRepository>>();
            _mapper = A.Fake<IMapper>();
        }

        private async Task<TasksContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TasksContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            //creating DBcontext from mock
            var dbContext = new TasksContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Entries.AsNoTracking();

            
            dbContext.Tasks.Add(new Task { Id = 1, Name = "Running", GroupId = 4, Created = DateTime.Now, ApplicationUserId = _userId });
            dbContext.Tasks.Add(new Task { Id = 2, Name = "Cooking", GroupId = 4, Created = DateTime.Now, ApplicationUserId = _userId });
            dbContext.Tasks.Add(new Task { Id = 3, Name = "Reading", GroupId = 4, Created = DateTime.Now, ApplicationUserId = _userId });

            dbContext.Entries.Add(new Entry { Id = _entryId_1, TaskId = _taskId_1, Duration = _duration, Created = _created, LastUpdated = null, Deleted = null });
            dbContext.Entries.Add(new Entry { Id = _entryId_2, TaskId = _taskId_2, Duration = _duration, Created = _created, LastUpdated = null, Deleted = null });
            dbContext.Entries.Add(new Entry { Id = _entryId_3, TaskId = _taskId_3, Duration = _duration, Created = _created, LastUpdated = null, Deleted = null });
            await dbContext.SaveChangesAsync();
            
            return dbContext;
        }

        [Fact]
        public async void Add_TakesValidParams_ReturnsTrue()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.Add(5, _durationObject, _created);
            //Assert
            dbContext.Entries.Should().HaveCount(4);
            result.Should().BeTrue();
        }

        [Fact]
        public async void Add_TakesInvalidParams_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.Add(23, _durationObject, _created);
            //Assert
            dbContext.Entries.Should().HaveCount(3);
            result.Should().BeFalse();
        }





    }
}
