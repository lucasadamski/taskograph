using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Web.Models;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;
using taskograph.Models.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using taskograph.Models;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class TargetController : Controller
    {
        private IPreciseTargetRepository _preciseTargetRepository;
        private IRegularTargetRepository _regularTargetRepository;
        private ITaskRepository _taskRepository;

        private readonly ILogger<TargetController> _logger;
        private IConfiguration _configuration;

        string _userId;


        public TargetController(IPreciseTargetRepository preciseTargetRepository, IRegularTargetRepository regularTargetRepository,
            ITaskRepository taskRepository, ILogger<TargetController> logger, IConfiguration configuration)
        {
            _preciseTargetRepository = preciseTargetRepository;
            _regularTargetRepository = regularTargetRepository;
            _logger = logger;
            _configuration = configuration;
            _taskRepository = taskRepository;
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
            _userId = GetIdentityUserId();
            TargetViewModel targetVM = new TargetViewModel();
            targetVM.TasksSLI = GetAllTasksSLI(_userId);
            targetVM.DateDue = DateTime.Today;
            return View("AddPreciseTarget", targetVM);
        }

        [HttpPost]
        public IActionResult AddPreciseTarget(TargetViewModel targetVM)
        {
            _userId = GetIdentityUserId();
            PreciseTarget preciseTarget = new PreciseTarget()
            {
                TaskId = targetVM.TaskId,
                Name = targetVM.Name,
                DateDue = targetVM.DateDue,
                Created = DateTime.Today
            };
            _preciseTargetRepository.Add(preciseTarget);
            return RedirectToAction("AddPreciseTarget");
        }

        public IActionResult AddRegularTarget()
        {
            return View("AddRegularTarget");
        }

        private List<SelectListItem> GetAllTasksSLI(string userId)
        {
            return _taskRepository.GetAll(userId)
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString()
                })
                .ToList();
        }
    }
}
