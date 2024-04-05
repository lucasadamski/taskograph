using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Security.Claims;
using taskograph.EF.Repositories;
using taskograph.EF.Repositories.Infrastructure;
using taskograph.Models.Tables;
using taskograph.Web.Models;
using taskograph.Web.Models.DTOs;
using Task = taskograph.Models.Tables.Task;
using static taskograph.Helpers.Messages;

namespace taskograph.Web.Controllers
{
    public class QuoteController : Controller
    {
        private IQuoteRepository _quoteRepository;

        private readonly ILogger<QuoteController> _logger;
        private IConfiguration _configuration;

        public QuoteController(IQuoteRepository quoteRepository, ILogger<QuoteController> logger, IConfiguration configuration)
        {
            _quoteRepository = quoteRepository;
            _logger = logger;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            QuoteViewModel quoteVM = new QuoteViewModel();
            quoteVM.Quotes = _quoteRepository.GetAll(_userId).Select(n => n.Name).ToList();
            return View(quoteVM);
        }
    }
}
