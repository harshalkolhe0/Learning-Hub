using LearningHubMVC.Models;
using LearningHubMVC.ServiceRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LearningHubMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static List<Course> courses = new List<Course>();
        public static List<Course> presentCourses = new List<Course>();
        public void GetCourses()
        {
            try
            {
                presentCourses.Clear();
                courses.Clear();
                if (HttpContext.Session.GetString("user") == null) return;
                string un = HttpContext.Session.GetString("user");
                ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), un);
                courses = serviceObj.GetResponse<Course>("Courses");
                foreach (var item in courses)
                {
                    if (item.startdate < DateTime.Now && item.enddate > DateTime.Now)
                    {
                        presentCourses.Add(item);
                    }
                }
                //ViewBag.PART = participants;
                ViewBag.COURSES = courses;
                ViewBag.TRAIN = presentCourses;
                ViewBag.Title = "All Products";

            }
            catch (Exception)
            {
                throw;
            }
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}