using Microsoft.AspNetCore.Mvc;

namespace server.Controllers
{
    public class HolidayRequestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
