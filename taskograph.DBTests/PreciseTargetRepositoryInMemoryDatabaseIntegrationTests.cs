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
        private readonly ILogger<TaskRepository> _logger;
        private readonly IMapper _mapper;
        private string _userIdOne = "testUserIdOne";
        private string _userIdTwo = "testUserIdTwo";


        private DateTime _created = new DateTime(2024, 01, 01);
        private DateTime _dueOctober = new DateTime(2024, 10, 01);
        private DateTime _dueNovember = new DateTime(2024, 11, 01);
        private DateTime _dueDecember = new DateTime(2024, 12, 01);

        public PreciseTargetRepositoryInMemoryDatabaseIntegrationTests()
        {
            _logger = A.Fake<ILogger<TaskRepository>>();
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

            dbContext.Tasks.Add(new Task { Id = 1, Name = "Running", GroupId = 4, Created = _created, ApplicationUserId = _userIdOne });
            dbContext.Tasks.Add(new Task { Id = 2, Name = "Cooking", GroupId = 4, Created = _created, ApplicationUserId = _userIdOne });
            dbContext.Tasks.Add(new Task { Id = 2, Name = "Reading", GroupId = 4, Created = _created, ApplicationUserId = _userIdTwo });

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
    }
}
