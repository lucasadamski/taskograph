using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Models;
using static taskograph.Helpers.Messages;
using System.Text;
using taskograph.Web.Models.Enums;
using taskograph.Web.Engine;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        private GraphGenerator _graphGenerator;

        public GraphController(ITaskRepository taskRepository, IEntryRepository entryRepository, ILogger<GraphController> logger, IConfiguration configuration)
        {
            _taskRepository = taskRepository;
            _entryRepository = entryRepository;
            _logger = logger;
            _configuration = configuration;
            _graphGenerator = new GraphGenerator(taskRepository, entryRepository);
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

            GraphViewModel graphVM = new GraphViewModel()
            {
                Start = DateTime.Now.AddDays(-7),
                End = DateTime.Now
            };

            return View(graphVM);
        }

        [HttpPost]
        public IActionResult ShowGraph(GraphViewModel graphVM)
        {
            DateTime Start = graphVM.Start;
            DateTime End = graphVM.End;
            //sanitize date
            if (Start >= End)
            {
                Start = DateTime.Now.AddDays(-7);
                End = DateTime.Now;
            }

            graphVM.Tables = _graphGenerator.GenerateTables(GraphTimeUnit.Month, GetIdentityUserId(), Start, End);
            graphVM.GraphDescription = _graphGenerator.GenerateGraphDescription(Start, End, GraphTimeUnit.Month);         // TODO 
            return View("ShowGraph", graphVM);
        }

        public List<SelectListItem> GraphTimeUnitToSLI()
        {
            GraphTimeUnit[] allEnums = Enum.GetValues<GraphTimeUnit>();

            List<SelectListItem> result = new List<SelectListItem>();

            foreach (var item in allEnums)
            {
                result.Add(new SelectListItem()
                {
                    Value = ((int)item).ToString(),
                    Text = item.ToString()
                });
            }

            return result;
        }
    }
}
