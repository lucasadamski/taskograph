using Microsoft.AspNetCore.Mvc;

namespace taskograph.Web.Controllers
{
    public class TargetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
