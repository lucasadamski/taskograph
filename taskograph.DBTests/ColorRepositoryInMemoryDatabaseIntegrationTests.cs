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
    public class ColorRepositoryInMemoryDatabaseIntegrationTests
    {
        private readonly ILogger<ColorRepository> _logger;

        public ColorRepositoryInMemoryDatabaseIntegrationTests()
        {
            _logger = A.Fake<ILogger<ColorRepository>>();
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

            dbContext.Colors.Add(new Color { Id = 1, Name = "Red" });
            dbContext.Colors.Add(new Color { Id = 2, Name = "Green" });
            dbContext.Colors.Add(new Color { Id = 3, Name = "Blue" });
            dbContext.Colors.Add(new Color { Id = 4, Name = "Yellow" });
            dbContext.Colors.Add(new Color { Id = 5, Name = "Grey" });
            dbContext.Colors.Add(new Color { Id = 6, Name = "Brown" });
            dbContext.Colors.Add(new Color { Id = 7, Name = "Orange" });
            dbContext.Colors.Add(new Color { Id = 8, Name = "Pink" });
            dbContext.Colors.Add(new Color { Id = 9, Name = "Purple" });
            dbContext.Colors.Add(new Color { Id = 10, Name = "Beige" });

            await dbContext.SaveChangesAsync();

            return dbContext;
        }

        [Fact]
        public async void Add_TakesString_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var colorRepository = new ColorRepository(dbContext, _logger);
            //Act


            //Assert
        }
    }
}
