using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Models;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using taskograph.Web.Models.DTOs;
using taskograph.Models.Tables;
using Microsoft.IdentityModel.Tokens;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class GraphController : Controller
    {
        private ITaskRepository _taskRepository;
        private IEntryRepository _entryRepository;
        private IDurationRepository _durationRepository;

        private readonly ILogger<GraphController> _logger;
        private IConfiguration _configuration;

        string _userId;

        public GraphController(ITaskRepository taskRepository, IEntryRepository entryRepository,
            IDurationRepository durationRepository, ILogger<GraphController> logger, IConfiguration configuration)
        {
            _taskRepository = taskRepository;
            _entryRepository = entryRepository;
            _durationRepository = durationRepository;
            _logger = logger;
            _configuration = configuration;

        }
        public IActionResult Index()
        {
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            GraphViewModel graphVM = new GraphViewModel();

            //fixed for 7 days
            //load 7 days of tasks
            List<Task> tasks = new List<Task>();
            tasks = _taskRepository.GetAll(_userId).ToList();
            if (tasks.IsNullOrEmpty())
            {
                return View("CustomErrorPage", ERROR_NO_TASKS);
            }
            DateTime day = DateTime.Now.AddDays(-7);

            TextGraphOneCellDTO temp = new TextGraphOneCellDTO();
            for (int i = 0; i < 7; i++)
            {
                temp = new TextGraphOneCellDTO()
                {
                    Description = $"{day.AddDays(i + 1).DayOfWeek} {day.AddDays(i + 1).Date.ToString("dd-MM-yy")}",
                    Tasks = ConvertTasksToDTO(tasks, day.AddDays(i + 1)),
                    TotalDuration = GetTotalDurationFromTasks(ConvertTasksToDTO(tasks, day.AddDays(i + 1)))
                };
                graphVM.TextGraphCell.Add(temp);
            }
            

            return View(graphVM);
        }

        private List<TaskDTO> ConvertTasksToDTO(List<Task> input, DateTime date)
        {
            return input.Select(n => new TaskDTO()
            {
                Id = n.Id,
                Name = n.Name,
                Group = n.Group?.Name ?? NULL_VALUE,
                TotalDurationToday = (_entryRepository.GetTotalDurationForTask(n.Id, date))
            })
                .ToList();
        }

        private Duration GetTotalDurationFromTasks(List<TaskDTO> input)
        {
            if (input.IsNullOrEmpty() == true)
            {
                return new Duration();
            }
            else
            {
                List<Duration> durations = input.Select(n => n.TotalDurationToday).ToList();
                return durations.Aggregate((a, b) => a + b);
            }

        }

        
    }
}
