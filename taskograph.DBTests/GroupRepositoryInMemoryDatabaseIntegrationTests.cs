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
    public class GroupRepositoryInMemoryDatabaseIntegrationTests
    {
        private readonly ILogger<GroupRepository> _logger;
        private string _userIdOne = "userIdOne";
        private string _userIdTwo = "userIdTwo";
        private string _groupName = "Group number ";
        private DateTime _created = new DateTime(2024, 01, 01);

        public GroupRepositoryInMemoryDatabaseIntegrationTests()
        {
            _logger = A.Fake<ILogger<GroupRepository>>();
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

            dbContext.Groups.Add(new Group { Id = 1, Name = _groupName + "1", ApplicationUserId = _userIdOne, Created = _created });
            dbContext.Groups.Add(new Group { Id = 2, Name = _groupName + "2", ApplicationUserId = _userIdOne, Created = _created });
            dbContext.Groups.Add(new Group { Id = 3, Name = _groupName + "3", ApplicationUserId = _userIdOne, Created = _created });
            dbContext.Groups.Add(new Group { Id = 4, Name = _groupName + "4", ApplicationUserId = _userIdOne, Created = _created });
            dbContext.Groups.Add(new Group { Id = 5, Name = _groupName + "5", ApplicationUserId = _userIdOne, Created = _created });
            dbContext.Groups.Add(new Group { Id = 6, Name = _groupName + "6", ApplicationUserId = _userIdTwo, Created = _created });
            dbContext.Groups.Add(new Group { Id = 7, Name = _groupName + "7", ApplicationUserId = _userIdTwo, Created = _created });
            dbContext.Groups.Add(new Group { Id = 8, Name = _groupName + "8", ApplicationUserId = _userIdTwo, Created = _created });
            dbContext.Groups.Add(new Group { Id = 9, Name = _groupName + "9", ApplicationUserId = _userIdTwo, Created = _created });
            dbContext.Groups.Add(new Group { Id = 10, Name = _groupName + "10", ApplicationUserId = _userIdTwo, Created = _created });

            await dbContext.SaveChangesAsync();

            return dbContext;
        }

        [Fact]
        public async void Add_TakesValidGroup_ReturnsTrue()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);
            var groupItem = new Group() {Id = 123,  Name = "New Group Item", ApplicationUserId = "NewTesUser" };
            // Act
            var result = groupRepository.Add(groupItem);

            // Assert
            result.Should().Be(true);
            dbContext.Groups.Count().Should().Be(11);
        }

        [Fact]
        public async void Add_TakesRepeatedIdGroup_ReturnsFalse()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);
            var groupItem = new Group() { Id = 1, Name = "New Group Item", ApplicationUserId = "NewTesUser" };
            // Act
            var result = groupRepository.Add(groupItem);

            // Assert
            result.Should().Be(false);
            dbContext.Groups.Count().Should().Be(10);
        }

        [Fact]
        public async void Add_TakesNull_ReturnsFalse()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);
            
            // Act
            var result = groupRepository.Add(null);

            // Assert
            result.Should().Be(false);
            dbContext.Groups.Count().Should().Be(10);
        }

        [Fact]
        public async void Delete_TakesValidGroup_ReturnsTrue()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);
            var groupItem = groupRepository.Get(1);
            // Act
            var result = groupRepository.Delete(groupItem);
            groupItem = groupRepository.Get(1);
            // Assert
            result.Should().Be(true);
            groupItem.Deleted.Should().NotBe(null);
        }

        [Fact]
        public async void Delete_TakesNonExistingGroup_ReturnsFalse()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);
            var groupItem = new Group() { Name = "Non Existing Group" };
            // Act
            var result = groupRepository.Delete(groupItem);
       
            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Delete_TakesNull_ReturnsFalse()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);
            
            // Act
            var result = groupRepository.Delete(null);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Edit_TakesValidGroup_ReturnsTrue()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);
            var groupItem = groupRepository.Get(1);
            groupItem.Name = "Edited";
            // Act
            var result = groupRepository.Edit(groupItem);
            groupItem = groupRepository.Get(1);
            // Assert
            result.Should().Be(true);
            groupItem.Name.Should().Be("Edited");
        }

        [Fact]
        public async void Edit_TakesNonExistingGroup_ReturnsFalse()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);
            var groupItem = new Group() { Name = "Non Existing Group" };
            // Act
            var result = groupRepository.Edit(groupItem);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Edit_TakesNull_ReturnsFalse()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);

            // Act
            var result = groupRepository.Edit(null);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Get_TakesValidId_ReturnsGroup()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);

            // Act
            var result = groupRepository.Get(1);

            // Assert
            result.Should().BeOfType(typeof(Group));
            result.Name.Should().Be(_groupName + "1");
        }

        [Fact]
        public async void Get_TakesInvalidId_ReturnsEmptyGroup()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);

            // Act
            var result = groupRepository.Get(3421);

            // Assert
            result.Should().BeOfType(typeof(Group));
            result.Name.Should().Be(null);
        }

        [Fact]
        public async void GetAll_TakesValidUserId_ReturnsCollection()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);

            // Act
            var result = groupRepository.GetAll(_userIdOne);

            // Assert
            result.Should().BeOfType(typeof(List<Group>));
            result.Count().Should().Be(5);
        }

        [Fact]
        public async void GetAll_TakesNonExistingUserId_ReturnsEmptyCollection()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);

            // Act
            var result = groupRepository.GetAll("Non existing user id");

            // Assert
            result.Should().BeOfType(typeof(List<Group>));
            result.Count().Should().Be(0);
        }

        [Fact]
        public async void GetAll_TakesNull_ReturnsEmptyCollection()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var groupRepository = new GroupRepository(dbContext, _logger);

            // Act
            var result = groupRepository.GetAll(null);

            // Assert
            result.Should().BeOfType(typeof(List<Group>));
            result.Count().Should().Be(0);
        }

    }
}
