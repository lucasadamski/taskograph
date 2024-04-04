using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Models;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class TargetController : Controller
    {
        private IPreciseTargetRepository _preciseTargetRepository;
        

        private readonly ILogger<TargetController> _logger;
        private IConfiguration _configuration;


        public TargetController(IPreciseTargetRepository preciseTargetRepository, ILogger<TargetController> logger, IConfiguration configuration)
        {
            _preciseTargetRepository = preciseTargetRepository;
            _logger = logger;
            _configuration = configuration;


        }
        public IActionResult Index()
        {
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            TargetViewModel targetVM = new TargetViewModel();

            targetVM.PreciseTargets = _preciseTargetRepository.GetAll(_userId).ToList();

            return View(targetVM);
        }

        private TimeSpan CalculatePrectiseTargetStatus(DateTime dateCreated, DateTime dateDue)
        {
            TimeSpan result = dateDue - dateCreated;
            return result;
        }
    }
}
