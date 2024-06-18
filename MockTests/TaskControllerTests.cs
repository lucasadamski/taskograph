using Microsoft.Extensions.Logging;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Controllers;
using Xunit;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Task = taskograph.Models.Tables.Task;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using taskograph.Helpers;


namespace taskograph.Tests
{
    public class TaskControllerTests
    {
        private ITaskRepository _taskRepository;
        private IEntryRepository _entryRepository;
        private IDurationRepository _durationRepository;
        private IGroupRepository _groupRepository;
        private IColorRepository _colorRepository;
        private IConfiguration _configuration;
        private ILogger<TaskController> _logger;

        private TaskController _taskController;

        public TaskControllerTests()
        {
            _taskRepository = A.Fake<ITaskRepository>();
            _entryRepository = A.Fake<IEntryRepository>(); ;
            _durationRepository = A.Fake<IDurationRepository>(); ;
            _groupRepository = A.Fake<IGroupRepository>(); ;
            _colorRepository = A.Fake<IColorRepository>(); 
            _configuration = A.Fake<IConfiguration>();
            _logger = A.Fake<ILogger<TaskController>>();

            //SUT
            _taskController = new TaskController(_taskRepository, _entryRepository, _durationRepository, 
                _groupRepository, _colorRepository, _logger, _configuration );
        }

        [Fact]
        public void Index_ReturnsSuccess()
        {
            //Arrange
            var tasks = A.Fake<IEnumerable<Task>>();
            A.CallTo(() => _taskRepository.GetAll(TestHelpers.TEST_PARAMETER)).Returns(tasks);
            //Act
            var actual = _taskController.Index();
            //Assert
            actual.Should().BeOfType<ViewResult>();
        }

        
    }
}
