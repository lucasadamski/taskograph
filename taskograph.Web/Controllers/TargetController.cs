using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Models;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class TargetController : Controller
    {
        private IPreciseTargetRepository _preciseTargetRepository;
        private IRegularTargetRepository _regularTargetRepository;

        private readonly ILogger<TargetController> _logger;
        private IConfiguration _configuration;

        string _userId;


        public TargetController(IPreciseTargetRepository preciseTargetRepository, IRegularTargetRepository regularTargetRepository,
            ILogger<TargetController> logger, IConfiguration configuration)
        {
            _preciseTargetRepository = preciseTargetRepository;
            _regularTargetRepository = regularTargetRepository;
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

            TargetViewModel targetVM = new TargetViewModel();

            targetVM.PreciseTargets = _preciseTargetRepository.GetAll(_userId).ToList();
            targetVM.RegularTargets = _regularTargetRepository.Get(_userId).ToList();

            return View(targetVM);
        }

        public IActionResult AddPreciseTarget()
        {
            return View("AddPreciseTarget");
        }

        public IActionResult AddRegularTarget()
        {
            return View("AddRegularTarget");
        }
    }
}
