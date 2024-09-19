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

    }
}
