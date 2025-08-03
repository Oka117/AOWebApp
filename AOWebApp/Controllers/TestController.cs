using Microsoft.AspNetCore.Mvc;

namespace AOWebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
