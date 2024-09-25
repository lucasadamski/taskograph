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
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace taskograph.RepositoriesInMemoryDatabaseIntegrationTests
{
    public class PreciseTargetRepositoryInMemoryDatabaseIntegrationTests
    {
        private readonly ILogger<PreciseTargetRepository> _logger;
        private readonly IMapper _mapper;
        private string _userIdOne = "testUserIdOne";
        private string _userIdTwo = "testUserIdTwo";


        private DateTime _created = new DateTime(2024, 01, 01);
        private DateTime _dueOctober = new DateTime(2024, 10, 01);
        private DateTime _dueNovember = new DateTime(2024, 11, 01);
        private DateTime _dueDecember = new DateTime(2024, 12, 01);

        public PreciseTargetRepositoryInMemoryDatabaseIntegrationTests()
        {
            _logger = A.Fake<ILogger<PreciseTargetRepository>>();
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
            dbContext.Tasks.AsNoTracking();
            dbContext.PreciseTargets.AsNoTracking();

            dbContext.Tasks.Add(new Task { Id = 1, Name = "Running", GroupId = 4, Created = _created, ApplicationUserId = _userIdOne });
            dbContext.Tasks.Add(new Task { Id = 2, Name = "Cooking", GroupId = 4, Created = _created, ApplicationUserId = _userIdOne });
            dbContext.Tasks.Add(new Task { Id = 3, Name = "Reading", GroupId = 4, Created = _created, ApplicationUserId = _userIdTwo });

            dbContext.PreciseTargets.Add(new PreciseTarget { Id = 1, Name = "Run 1", TaskId = 1, DateDue = _dueOctober, Created =  _created});
            dbContext.PreciseTargets.Add(new PreciseTarget { Id = 2, Name = "Run 2", TaskId = 1, DateDue = _dueNovember, Created = _created });
            dbContext.PreciseTargets.Add(new PreciseTarget { Id = 3, Name = "Run 3", TaskId = 1, DateDue = _dueDecember, Created = _created });
            dbContext.PreciseTargets.Add(new PreciseTarget { Id = 4, Name = "Cook 1", TaskId = 2, DateDue = _dueOctober, Created = _created });
            dbContext.PreciseTargets.Add(new PreciseTarget { Id = 5, Name = "Cook 2", TaskId = 2, DateDue = _dueNovember, Created = _created });
            dbContext.PreciseTargets.Add(new PreciseTarget { Id = 6, Name = "Cook 3", TaskId = 2, DateDue = _dueDecember, Created = _created });

            dbContext.PreciseTargets.Add(new PreciseTarget { Id = 7, Name = "Read 1", TaskId = 3, DateDue = _dueOctober, Created = _created });
            dbContext.PreciseTargets.Add(new PreciseTarget { Id = 8, Name = "Read 2", TaskId = 3, DateDue = _dueNovember, Created = _created });
            dbContext.PreciseTargets.Add(new PreciseTarget { Id = 9, Name = "Read 3", TaskId = 3, DateDue = _dueDecember, Created = _created });

            await dbContext.SaveChangesAsync();

            return dbContext;
        }

        [Fact]
        public async void Add_TakesValidObject_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Add(new PreciseTarget() { Name = "TestTarget", TaskId = 1, DateDue = _dueOctober, Created = DateTime.Now});
            //Assert
            result.Should().Be(true);
            dbContext.PreciseTargets.Count().Should().Be(10);
        }

        [Fact]
        public async void Add_TakesInvalidString_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Add(new PreciseTarget() {TaskId = 1, DateDue = _dueOctober });
            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Add_TakesNull_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Add(null);
            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Delete_TakesValidObject_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            var targetObject = preciseTargetRepository.Get(1);

            //Act
            var result = preciseTargetRepository.Delete(targetObject);
            //Assert
            result.Should().Be(true);
            targetObject = preciseTargetRepository.Get(1);
            targetObject.Deleted.Should().NotBe(null);
        }


        [Fact]
        public async void Delete_TakesNonExistentObject_ReturnsTrue()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Delete(new PreciseTarget() { Name = "testItem", TaskId = 1, DateDue = _dueOctober });
            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Delete_TakesInvalidObject_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Delete(new PreciseTarget() {TaskId = 1, DateDue = _dueOctober });
            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Edit_TakesValidObject_ReturnsTrue()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            var targetObject = preciseTargetRepository.Get(1);
            targetObject.Name = "Modified object";
            //Act
            var result = preciseTargetRepository.Edit(targetObject);
            //Assert
            result.Should().Be(true);
            targetObject = preciseTargetRepository.Get(1);
            targetObject.LastUpdated.Should().NotBe(null);
            targetObject.Name.Should().Be("Modified object");
        }

        [Fact]
        public async void Edit_TakesInvalidObject_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Edit(new PreciseTarget() { Name = "testItem", TaskId = 1, DateDue = _dueOctober });
            //Assert
            result.Should().Be(false);
        }


        [Fact]
        public async void Edit_TakesNull_ReturnsFalse()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Edit(null);
            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Get_TakesValidId_ReturnsValidObject()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Get(1);
            //Assert
            result.Name.Should().Be("Run 1");
        }

        [Fact]
        public async void Get_TakesInvalidId_ReturnsEmptyObject()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Get(12423);
            //Assert
            result.Name.Should().Be(null);
        }

        [Fact]
        public async void Get_TakesDateRange_ReturnsSixObjects()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Get(_userIdOne, _dueOctober, _dueDecember);
            //Assert
            result.Count().Should().Be(6);
        }


        [Fact]
        public async void Get_TakesDateRange_ReturnsThreeObjects()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Get(_userIdTwo, _dueOctober, _dueDecember);
            //Assert
            result.Count().Should().Be(3);
        }

        [Fact]
        public async void Get_TakesDateRange_ReturnsOneObject()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Get(_userIdTwo, _dueDecember, _dueDecember);
            //Assert
            result.Count().Should().Be(1);
        }

        [Fact]
        public async void Get_TakesDateRange_ReturnsEmptyList()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Get(_userIdTwo, _dueDecember.AddDays(1) , _dueDecember.AddDays(2));
            //Assert
            result.Count().Should().Be(0);
        }

        [Fact]
        public async void Get_TakesDateRangeWithNonExistentUserId_ReturnsEmptyList()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.Get("Non existing user id", _dueOctober, _dueDecember);
            //Assert
            result.Count().Should().Be(0);
        }

        [Fact]
        public async void GetAll_TakesUserId_ReturnsSixElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.GetAll(_userIdOne);
            //Assert
            result.Should().BeOfType(typeof(List<PreciseTarget>));
            result.Count().Should().Be(6);
        }

        [Fact]
        public async void GetAll_TakesNonExistingUserId_ReturnsZeroElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.GetAll("non existing user id");
            //Assert
            result.Should().BeOfType(typeof(List<PreciseTarget>));
            result.Count().Should().Be(0);
        }

        [Fact]
        public async void GetAll_TakesNull_ReturnsZeroElements()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var preciseTargetRepository = new PreciseTargetRepository(dbContext, _logger, _mapper);
            //Act
            var result = preciseTargetRepository.GetAll("non existing user id");
            //Assert
            result.Should().BeOfType(typeof(List<PreciseTarget>));
            result.Count().Should().Be(0);
        }



    }
}
