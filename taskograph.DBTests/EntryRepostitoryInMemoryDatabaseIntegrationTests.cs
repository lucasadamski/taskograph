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
    public class EntryRepostitoryInMemoryDatabaseIntegrationTests
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

        private DateTime _createdFirstEntry = new DateTime(2000, 1, 1);
        private DateTime _createdSecondEntry = new DateTime(2000, 2, 1);
        private DateTime _createdThirdEntry = new DateTime(2000, 3, 1);

        private DateTime? _lastUpdated = new DateTime(2020, 1, 1);
        private string _userId = "testUserId";

        public EntryRepostitoryInMemoryDatabaseIntegrationTests()
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

            
            dbContext.Tasks.Add(new Task { Id = 1, Name = "RunningId1", GroupId = 4, Created = _createdFirstEntry, ApplicationUserId = _userId });
            dbContext.Tasks.Add(new Task { Id = 2, Name = "CookingId2", GroupId = 4, Created = _createdSecondEntry, ApplicationUserId = _userId });
            dbContext.Tasks.Add(new Task { Id = 3, Name = "ReadingId3", GroupId = 4, Created = _createdThirdEntry, ApplicationUserId = _userId });

            dbContext.Entries.Add(new Entry { Id = _entryId_1, TaskId = _taskId_1, Duration = _duration, Created = _createdFirstEntry, LastUpdated = null, Deleted = null });
            dbContext.Entries.Add(new Entry { Id = _entryId_2, TaskId = _taskId_2, Duration = _duration, Created = _createdSecondEntry, LastUpdated = null, Deleted = null });
            dbContext.Entries.Add(new Entry { Id = _entryId_3, TaskId = _taskId_3, Duration = _duration, Created = _createdThirdEntry, LastUpdated = null, Deleted = null });
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
            var result = entryRepository.Add(1, _durationObject, _createdFirstEntry.AddDays(1));
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
            var result = entryRepository.Add(23, _durationObject, _createdFirstEntry);
            //Assert
            dbContext.Entries.Should().HaveCount(3);
            result.Should().BeFalse();
        }

        [Fact]
        public async void Add_TakesNullDuration_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.Add(1, null, _createdFirstEntry);
            //Assert
            dbContext.Entries.Should().HaveCount(3);
            result.Should().BeFalse();
        }

        [Fact]
        public async void Delete_TakesValidTask_ReturnsTrue()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            var entry = entryRepository.Get(1);
            //Act
            var result = entryRepository.Delete(entry);
            //Assert
            dbContext.Entries.Should().HaveCount(3);
            entry.Deleted.Should().NotBe(null);
            result.Should().BeTrue();
        }

        [Fact]
        public async void Delete_TakesNull_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.Delete(null);
            //Assert
            dbContext.Entries.Should().HaveCount(3);
            result.Should().BeFalse();
        }

        [Fact]
        public async void Edit_TakesValidEntry_ReturnsTrue_EditedElementSaved()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            var entry = entryRepository.Get(1);
            entry.Duration = 9999L;
            //Act
            var result = entryRepository.Edit(entry);
            //Assert
            dbContext.Entries.Should().HaveCount(3);
            result.Should().Be(true);
            entryRepository.Get(1).Duration.Should().Be(9999L);
        }

        [Fact]
        public async void Edit_TakesNull_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.Edit(null);
            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Get_TakesValidId_ReturnsValidEntry()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.Get(1);
            //Assert
            result.Should().BeOfType(typeof(Entry));
            result.Id.Should().Be(1);
        }

        [Fact]
        public async void Get_TakesInvalidId_ReturnsEmptyEntry()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.Get(1123);
            //Assert
            result.Should().BeOfType(typeof(Entry));
            result.Id.Should().Be(0);
        }

        [Fact]
        public async void Get_TakesUserId_ReturnsList()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.Get(_userId, _createdFirstEntry, _createdThirdEntry);
            //Assert
            result.Should().BeOfType(typeof(List<Entry>));
            result.Count().Should().Be(3);
        }

        /*********black  hole*******/

        [Fact]
        public async void GetByGroup_TakesGroupId_ReturnsListWithThreeElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.GetByGroup(4, _createdFirstEntry, _createdThirdEntry);
            //Assert
            result.Should().BeOfType(typeof(List<Entry>));
            result.Count().Should().Be(3);
        }

        [Fact]
        public async void GetByGroup_TakesGroupId_ReturnsListWithZeroElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            //Act
            var result = entryRepository.GetByGroup(67, _createdFirstEntry, _createdThirdEntry);
            //Assert
            result.Should().BeOfType(typeof(List<Entry>));
            result.Count().Should().Be(0);
        }

        [Theory]
        [InlineData(1, 2000, 1, 1, 100)]
        [InlineData(1234, 2000, 1, 1, 0)]
        [InlineData(1, 2033, 1, 1, 0)]
        public async void GetTotalDurationForTask_TakesTaskId_ReturnsLong(int taskId, int year, int month, int day, long expected)
        {
            // Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            var date = new DateTime(year, month, day);
            // Act
            var result = entryRepository.GetTotalDurationForTask(taskId, date);
            // Assert
            result.Should().BeOfType(typeof(long));
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("testUserId", 2000, 1, 1, 100)]
        [InlineData("testUserId", 2022, 1, 1, 0)]
        [InlineData("invalidId", 2020, 1, 1, 0)]
        public async void GetTotalDurationForAllTasks_TakesUserId_ReturnsLong(string userId, int year, int month, int day, long expected)
        {
            // Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            var date = new DateTime(year, month, day);
            // Act
            var result = entryRepository.GetTotalDurationForAllTasks(userId, date);
            // Assert
            result.Should().BeOfType(typeof(long));
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 2000, 1, 1, 2000, 1, 1, 100)]
        [InlineData(1, 2020, 1, 1, 2000, 1, 1, 0)]
        [InlineData(1, 1999, 1, 1, 1999, 10, 1, 0)]
        public async void GetTotalDurationForTask_TakesUserId_ReturnsLong(int taskId, int yearFrom, int monthFrom, int dayFrom,
             int yearTo, int monthTo, int dayTo, long expected)
        {
            // Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            var dateFrom = new DateTime(yearFrom, monthFrom, dayFrom);
            var dateTo = new DateTime(yearTo, monthTo, dayTo);
            // Act
            var result = entryRepository.GetTotalDurationForTask(taskId, dateFrom, dateTo);
            // Assert
            result.Should().BeOfType(typeof(long));
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 2000, 1, 1, 100L)]
        [InlineData(2, 2000, 2, 1, 100L)]
        [InlineData(454, 2000, 1, 1, null)]
        public async void GetExistingEntry_TakesTaskId_ReturnsEntry(int taskId, int year, int month, int day, long? expected)
        {
            // Arrange
            var dbContext = await GetDbContext();
            var entryRepository = new EntryRepository(dbContext, _logger, _mapper);
            var date = new DateTime(year, month, day);
            // Act
            var result = entryRepository.GetExistingEntry(taskId, date);
            // Assert
            result?.Duration.Should().Be(expected);
        }
    }
}
