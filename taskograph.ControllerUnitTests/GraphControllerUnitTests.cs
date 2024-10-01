using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models;
using taskograph.Models.DTOs;
using taskograph.Models.Tables;
using taskograph.Web.Controllers;
using taskograph.Web.Models;
using taskograph.Web.Models.DTOs;

namespace taskograph.ControllerUnitTests
{
    public class GraphControllerUnitTests
    {
        private ITaskRepository _taskRepository;
        private IEntryRepository _entryRepository;

        private readonly ILogger<GraphController> _logger;
        private IConfiguration _configuration;

        private GraphController _graphController;

        string _userId = "test user";

        public GraphControllerUnitTests()
        {
            _taskRepository = A.Fake<ITaskRepository>();
            _entryRepository = A.Fake<IEntryRepository>();
            _logger = A.Fake<ILogger<GraphController>>();
            _configuration = A.Fake<IConfiguration>();

            // Subject Under Testing
            _graphController = new GraphController(_taskRepository, _entryRepository, _logger, _configuration);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = _graphController.Index();
            // Assert
            result.Should().BeOfType(typeof(ViewResult));
        }
    }
}
