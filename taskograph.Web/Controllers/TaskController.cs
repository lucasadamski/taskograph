using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace taskograph.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
