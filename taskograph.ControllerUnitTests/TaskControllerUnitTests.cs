using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models;
using taskograph.Web.Controllers;
using taskograph.Web.Models;
using taskograph.Web.Models.DTOs;

namespace taskograph.ControllerUnitTests
{
    public class TaskControllerUnitTests
    {
        private ITaskRepository _taskRepository;
        private IEntryRepository _entryRepository;
        private IGroupRepository _groupRepository;
        private IColorRepository _colorRepository;
        private readonly ILogger<TaskController> _logger;
        private IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private TaskController _taskController;

        private string _userId = "test user";
        public TaskControllerUnitTests()
        {
            // Dependencies
            _taskRepository = A.Fake<ITaskRepository>();
            _entryRepository = A.Fake<IEntryRepository>();
            _groupRepository = A.Fake<IGroupRepository>();
            _colorRepository = A.Fake<IColorRepository>();
            _logger = A.Fake<ILogger<TaskController>>();
            _configuration = A.Fake<IConfiguration>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();

            

            // SUT
            _taskController = new TaskController(_taskRepository, _entryRepository, _groupRepository, _colorRepository, _logger, _configuration);

        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            TaskViewModel taskViewModel = new();
            var durations = A.Fake<List<long>>();
            durations = new List<long>() { 5, 10, 15, 30, 60, 120, 180, 240 };
            taskViewModel.Durations = durations;
            taskViewModel.Tasks = A.Fake<List<TaskDTO>>();
            A.CallTo(() => _taskRepository.GetAllTaskDTOs(_userId)).Returns(taskViewModel.Tasks);
         
            // Act
            var result = _taskController.Index();
            // Assert
            result.Should().BeOfType(typeof(ViewResult));
        }

        [Fact]
        public void AddEntry_ReturnsViewResult()
        {
            // Arrange
            int taskId = 1;
            Duration duration = new Duration() { Minutes = 100 };
            
            // Act
            IActionResult result = _taskController.AddEntry(taskId, duration);

            // Assert
            result.Should().BeOfType(typeof(ViewResult));
        }

        [Fact]
        public void ConfigTasks_ReturnsViewResult()
        {
            // Arrange

            // Act
            IActionResult result = _taskController.ConfigTasks();

            // Assert
            result.Should().BeOfType(typeof(ViewResult));
        }

        [Fact]
        public void AddTask_ReturnsViewResult()
        {
            // Arrange

            // Act
            IActionResult result = _taskController.AddTask();

            // Assert
            result.Should().BeOfType(typeof(ViewResult));
        }

        [Fact]
        public void AddGroup_ReturnsViewResult()
        {
            // Arrange

            // Act
            IActionResult result = _taskController.AddGroup();

            // Assert
            result.Should().BeOfType(typeof(ViewResult));
        }

        [Fact]
        public void AddGroupPOST_ReturnsRedirectToAction()
        {
            // Arrange
            TaskViewModel taskVM = new();

            // Act
            IActionResult result = _taskController.AddGroup(taskVM);

            // Assert
            result.Should().BeOfType(typeof(RedirectToActionResult));
        }

        [Fact]
        public void EditTask_ReturnsViewResult()
        {
            // Arrange

            // Act
            IActionResult result = _taskController.EditTask(1);

            // Assert
            result.Should().BeOfType(typeof(ViewResult));
        }

        [Fact]
        public void EditTaskPOST_ReturnsRedirectToAction()
        {
            // Arrange
            TaskViewModel taskVM = new() { TaskId = 1};

            // Act
            IActionResult result = _taskController.EditTask(taskVM);

            // Assert
            result.Should().BeOfType(typeof(RedirectToActionResult));
        }

        [Fact]
        public void EditGroup_ReturnsViewResult()
        {
            // Arrange

            // Act
            IActionResult result = _taskController.EditGroup(1);

            // Assert
            result.Should().BeOfType(typeof(ViewResult));
        }

        [Fact]
        public void EditGroupPOST_ReturnsRedirectToAction()
        {
            // Arrange
            TaskViewModel taskVM = new() { TaskId = 1 };

            // Act
            IActionResult result = _taskController.EditGroup(taskVM);

            // Assert
            result.Should().BeOfType(typeof(RedirectToActionResult));
        }
    }
}