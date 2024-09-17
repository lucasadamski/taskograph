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
        public async void Add_TakesValidString_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var colorRepository = new ColorRepository(dbContext, _logger);
            //Act
            var result = colorRepository.Add("newColor");
            //Assert
            result.Should().Be(true);
            colorRepository.GetAll().Count().Should().Be(11);
        }

        [Fact]
        public async void Add_TakesNullString_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var colorRepository = new ColorRepository(dbContext, _logger);
            //Act
            var result = colorRepository.Add(null);
            //Assert
            result.Should().Be(false);
            colorRepository.GetAll().Count().Should().Be(10);
        }

        [Fact]
        public async void Delete_TakesValidColor_ReturnsBool()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var colorRepository = new ColorRepository(dbContext, _logger);
            //Act
            var color = colorRepository.Get(1);
            var result = colorRepository.Delete(color);
            //Assert
            result.Should().Be(true);
            colorRepository.GetAll().Count().Should().Be(9);
        }

        [Fact]
        public async void Delete_TakesInvalidColor_ReturnsBool()
        {
            //Arranger
            var dbContext = await GetDbContext();
            var colorRepository = new ColorRepository(dbContext, _logger);
            //Act
            var result = colorRepository.Delete(null);
            //Assert
            result.Should().Be(false);
            colorRepository.GetAll().Count().Should().Be(10);
        }

        [Fact]
        public async void Edit_TakesValidColor_ReturnBool()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var colorRepository = new ColorRepository(dbContext, _logger);
            var testColor = "Test Color";

            // Act
            var colorItem = colorRepository.Get(1);
            colorItem.Name = testColor;
            var result = colorRepository.Edit(colorItem);

            // Assert
            result.Should().Be(true);
            colorRepository.GetAll().ElementAt(0).Name.Should().Be(testColor);
        }

        [Fact]
        public async void Edit_TakesInvalidColor_ReturnBool()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var colorRepository = new ColorRepository(dbContext, _logger);
            var testColor = "Test Color";

            // Act
            var colorItem = new Color();
            colorItem.Name = testColor;
            var result = colorRepository.Edit(colorItem);

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public async void Edit_TakesNull_ReturnBool()
        {
            // Arrange
            var dbContext = await GetDbContext();
            var colorRepository = new ColorRepository(dbContext, _logger);

            // Act  
            var result = colorRepository.Edit(null);

            // Assert
            result.Should().Be(false);
        }
    }
}
