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
    public class RegularTargetRepositoryInMemoryDatabaseIntegrationTests
    {
        private readonly ILogger<RegularTargetRepository> _logger;
        private readonly IMapper _mapper;
        private string _userIdOne = "testUserIdOne";

        private DateTime _createdOctober = new DateTime(2024, 10, 01);
        private DateTime _createdNovember = new DateTime(2024, 11, 01);
        private DateTime _createdDecember = new DateTime(2024, 12, 01);

        public RegularTargetRepositoryInMemoryDatabaseIntegrationTests()
        {
            _logger = A.Fake<ILogger<RegularTargetRepository>>();
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

            dbContext.Tasks.Add(new Task { Id = 1, Name = "Running", GroupId = 4, Created = _createdOctober, ApplicationUserId = _userIdOne });

            dbContext.RegularTargets.Add(new RegularTarget { Id = 1, TaskId = 1, Created = _createdOctober , TimeDedicatedToPerformTarget = 30, RegularTimeIntervalToAchieveTarget = 100 });
            dbContext.RegularTargets.Add(new RegularTarget { Id = 2, TaskId = 1, Created = _createdNovember, TimeDedicatedToPerformTarget = 30, RegularTimeIntervalToAchieveTarget = 100 });
            dbContext.RegularTargets.Add(new RegularTarget { Id = 3, TaskId = 1, Created = _createdDecember, TimeDedicatedToPerformTarget = 30, RegularTimeIntervalToAchieveTarget = 100 });

            await dbContext.SaveChangesAsync();

            return dbContext;
        }
    }
}
