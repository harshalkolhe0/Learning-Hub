using LearningHubMVC.Models;
using LearningHubMVC.ServiceRepository;
using Microsoft.AspNetCore.Mvc;

namespace LearningHubMVC.Controllers
{
    public class EnrollmentController : Controller
    {
        public static List<Enrollment>? myenrollments = new List<Enrollment>();
        public static List<Enrollment>? presentenrollments = new List<Enrollment>();
        public void GetMyEnrollments()
        {
            try
            {

                myenrollments.Clear();
                presentenrollments.Clear();
                if (HttpContext.Session.GetString("token") == null) return;
                string un = HttpContext.Session.GetString("user");
                ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), un);             //Enrollments/{un}/myenrollments
                myenrollments = serviceObj.GetMyEnrollments<Enrollment>("Enrollments/"+un+"/myenrollments");////update needed

                foreach (var item in myenrollments)
                {
                    if (item.Course.startdate <= DateTime.Now && item.Course.enddate >= DateTime.Now)
                    {
                        presentenrollments.Add(item);
                    }
                }
                
                ViewBag.presentenrollments = presentenrollments;
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Enrollment> GetPresentEnrollments()
        {
            GetMyEnrollments();
            return presentenrollments;
        }
        public List<Enrollment> MyEnrollments()
        {
            GetMyEnrollments();
            return myenrollments;
        }
        public IActionResult Enroll(int id)
        {
            if (HttpContext.Session.GetString("token") == null) return Redirect("/Courses/Index");
            string un = HttpContext.Session.GetString("user");
            Enrollment en = new Enrollment();
            en.CourseID = id;
            en.UserID = un;
            en.enrollmentDate = DateTime.Now;
            en.Course = null;
            en.User = null;
            
            
            ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), un);
            Course v = serviceObj.GetOneResponse<Course>("Courses/" + id.ToString());
            
            serviceObj.PostResponse("Enrollments", en);
            TempData["toastmsg"] = "Successfully , Enrolled for course '"+v.Name+"'";

            return Redirect("/Courses/Index");
        }
        public IActionResult Disenroll(int id)
        {
            if (HttpContext.Session.GetString("token") == null) return Redirect("/Courses/Index");
            string un = HttpContext.Session.GetString("user");
            ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), HttpContext.Session.GetString("user"));
            Enrollment e = serviceObj.GetOneResponse<Enrollment>("Enrollments/" + id.ToString());//it itself contains course no need of below 2 lines
            int cid = e.CourseID;
            Course v = serviceObj.GetOneResponse<Course>("Courses/" + cid.ToString());
            serviceObj.DeleteResponse("Enrollments/" + id.ToString());
            TempData["toastmsg"] = "Course '"+v.Name+"' Disenrolled Successfully!!!";
            return Redirect("/Courses/Index");
        }
            public IActionResult Index()
        {
            return View();
        }
    }
}
