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
using taskograph.Models;
using System.Runtime.CompilerServices;
using taskograph.Web.Models.Graph;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class GraphController : Controller
    {
        private ITaskRepository _taskRepository;
        private IEntryRepository _entryRepository;

        private readonly ILogger<GraphController> _logger;
        private IConfiguration _configuration;

        string _userId;

        public GraphController(ITaskRepository taskRepository, IEntryRepository entryRepository, ILogger<GraphController> logger, IConfiguration configuration)
        {
            _taskRepository = taskRepository;
            _entryRepository = entryRepository;
            _logger = logger;
            _configuration = configuration;

        }

        private string? GetIdentityUserId()
        {
            string? result = "test user";
            try
            {
                result = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            catch (Exception e)
            {
                _logger.LogError("Error message {0} Stack trace: {1}", e.Message, e.StackTrace);
            }
            return result;
        }

        public IActionResult Index()
        {
            _userId = GetIdentityUserId();

            if (_userId == null)
            {
                return View("CustomErrorPage", ERROR_NO_USER);
            }

            return View(new GraphViewModel());
        }

        [HttpPost]
        public IActionResult ShowGraph(GraphViewModel graphVM)
        {
            graphVM = GenerateGraph(1, GetIdentityUserId(), new DateTime(2024, 10, 8),
                new DateTime(2024, 10, 12));
            return View("ShowGraph", graphVM);
        }

        private List<TaskDTO> ConvertTasksToDTO(List<Task> input, DateTime date)
        {
            return input.Select(n => new TaskDTO()
            {
                Id = n.Id,
                Name = n.Name,
                Group = n.Group?.Name ?? NULL_VALUE,
                Duration = new Duration(_entryRepository.GetTotalDurationForTask(n.Id, date))
            })
                .ToList();
        }

        private long GetTotalDurationFromTasks(List<TaskDTO> input)
        {
            if (input.IsNullOrEmpty() == true)
            {
                return 0;
            }
            else
            {
                List<long> durations = input.Select(n => n.Duration.Minutes).ToList();
                return durations.Aggregate((a, b) => a + b);
            }

        }

        // howManyCalendarUnits - eg 2, 4, 3
        // calendarUnit - eg. week, month, year
        // => 2 weeks, 4 years etc.
        private GraphViewModel GenerateGraph(int calendarUnit, string _userId, DateTime from, DateTime to)
        {
            GraphViewModel graphVM = new GraphViewModel();

            List<Task> tasks = new List<Task>();
            tasks = _taskRepository.GetAll(_userId).ToList();
            if (tasks == null)
            {
                return graphVM;
            }
           
                /*temp = new Column()
                {
                    Title = $"{day.AddDays(i + 1).DayOfWeek} {day.AddDays(i + 1).Date.ToString("dd-MM-yy")}",
                    Tasks = ConvertTasksToDTO(tasks, day.AddDays(i + 1)),
                    DurationSummary = new Duration(GetTotalDurationFromTasks(ConvertTasksToDTO(tasks, day.AddDays(i + 1))))
                };
                graphVM.OneWeekTable.Add(temp);*/
            

            if (calendarUnit == 1)
            {
                // week
                // check beggining of a week
                // check end of the week
                // 
                Column column;
                Table table = new Table();
                while (from.DayOfWeek != DayOfWeek.Monday)
                {
                    from = from.AddDays(-1);
                }
                while (to.DayOfWeek != DayOfWeek.Sunday)
                {
                    to = to.AddDays(1);
                }
                while (from.Date != to.Date)
                {
                    column  = new Column()
                    {
                        Title = $"{from.DayOfWeek} {from.Date.ToString("dd-MM-yy")}",
                        Tasks = ConvertTasksToDTO(tasks, from),
                        DurationSummary = new Duration(GetTotalDurationFromTasks(ConvertTasksToDTO(tasks, from)))
                    };
                    table.Columns.Add(column);
                    if (from.DayOfWeek == DayOfWeek.Sunday)
                    {
                        graphVM.Tables.Add(table);
                        table = new Table();
                    }
                    from = from.AddDays(1);
                }

            }



           return graphVM;
            
        }
    }

}
