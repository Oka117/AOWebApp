using System.Diagnostics;
using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AOWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*throw new Exception("This is an error");*/
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult Test()
        //{
        //    // get the id from the Request url
        //    var id = Request.RouteValues["id"];

        //    // pass the id value to the view
        //    //ViewData.Add("id", id); // old way using dictionary of object values
        //    var text = Request.Query["text"]; // get the query parameter 'text'

        //    ViewBag.id = id; // new way using dynamic object properties
        //    ViewBag.searchText = text;

        //    return View();
        //}
        public IActionResult Test(int? id, string text)
        {
            // get the id from the Request url
            //var id = Request.RouteValues["id"];

            // pass the id value to the view
            //ViewData.Add("id", id); // old way using dictionary of object values
            ViewBag.id = id; // new way using dynamic object properties
            ViewBag.searchText = text; 


            return View();
        }
        public IActionResult RazorTest(int? id, string query, string form)
        {
            ViewBag.id = id;
            ViewBag.query = query;
            ViewBag.form = form;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
