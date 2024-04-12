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


        public TargetController(IPreciseTargetRepository preciseTargetRepository, IRegularTargetRepository regularTargetRepository,
            ILogger<TargetController> logger, IConfiguration configuration)
        {
            _preciseTargetRepository = preciseTargetRepository;
            _regularTargetRepository = regularTargetRepository;
            _logger = logger;
            _configuration = configuration;


        }
        public IActionResult Index()
        {
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            TargetViewModel targetVM = new TargetViewModel();

            targetVM.PreciseTargets = _preciseTargetRepository.GetAll(_userId).ToList();
            targetVM.RegularTargets = _regularTargetRepository.Get(_userId).ToList();

            return View(targetVM);
        }
    }
}
