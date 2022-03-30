using LearningHubMVC.Models;
using LearningHubMVC.ServiceRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LearningHubMVC.Controllers
{
    
    public class CoursesController : Controller
    {
        public static List<ShownCourse>? scourses = new List<ShownCourse>();
        public static List<ShownUser>? susers = new List<ShownUser>();
        public static List<ShownCourse>? presentSCourses = new List<ShownCourse>();
        public static List<Enrollment>? myenrollments = new List<Enrollment>();
        public static List<Enrollment>? presentenrollments = new List<Enrollment>();
        public static HashSet<int>? courseId = new HashSet<int>();
        public static Dictionary<int, int>? courseenroll = new Dictionary<int, int>();
        public void GetMyEnrollments()
        {
            try
            {

                myenrollments.Clear();
                presentenrollments.Clear();
                courseId.Clear();
                if (HttpContext.Session.GetString("token") == null) return;
                string un = HttpContext.Session.GetString("user");
                ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), HttpContext.Session.GetString("user"));             //Enrollments/{un}/myenrollments
                myenrollments = serviceObj.GetMyEnrollments<Enrollment>("Enrollments/" + un + "/myenrollments");

                foreach (var item in myenrollments)
                {
                    if (item.Course.startdate <= DateTime.Now && item.Course.enddate >= DateTime.Now)
                    {
                        courseId.Add(item.Course.ID);
                        courseenroll.Add(item.CourseID, item.EnrollmentID);
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
        /*
         * public static List<Course> courses = new List<Course>();
        public static List<Course> presentCourses = new List<Course>();
        public void GetCourses()
        {
            try
            {
                presentCourses.Clear();
                courses.Clear();
                ServiceRepo serviceObj = new ServiceRepo();
                courses = serviceObj.GetResponse<Course>("Courses");
                foreach (var item in courses)
                {
                    if (item.startdate <= DateTime.Now && item.enddate >= DateTime.Now)
                    {
                        presentCourses.Add(item);
                    }
                }
                
                //ViewBag.COURSES = courses;
                ViewBag.presentCourses = presentCourses;
                //ViewBag.Title = "All Products";

            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Course> GetPresentCourses()
        {
            GetCourses();
            return presentCourses;
        }
        public IActionResult Index()
        {
            GetCourses();
            return View(presentCourses);
        }
        */
        public void GetCourses()
        {
            try
            {
                presentSCourses.Clear();
                scourses.Clear();
                courseenroll.Clear();
                GetMyEnrollments();
                if (HttpContext.Session.GetString("token") == null) return;
                string un = HttpContext.Session.GetString("user");
                ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), un);
                List<Course> courses = serviceObj.GetResponse<Course>("Courses");
                foreach (var item in courses)
                {
                    if (item.startdate <= DateTime.Now && item.enddate >= DateTime.Now)
                    {
                        bool v = courseenroll.ContainsKey(item.ID);
                        int val = 0;
                        if(v) val= courseenroll[item.ID];
                        bool own = false;
                        if(item.Trainer.Contains(new Trainer(un,item.ID)))
                        {
                            own = true;
                        }
                        presentSCourses.Add(item.ToShownCourse(v, val,own));
                    }
                }

                //ViewBag.COURSES = courses;
                ViewBag.presentSCourses = presentSCourses;
                //ViewBag.Title = "All Products";

            }
            catch (Exception Exp)
            {
                String s = Exp.Message;
                throw;
            }
        }
        
        public List<ShownCourse> GetPresentCourses()
        {
            GetCourses();
            return presentSCourses;
        }
        public IActionResult Index()
        {
            GetCourses();
            return View(presentSCourses);
        }
        
        public IActionResult GetValues()
        {
            string un = HttpContext.Session.GetString("user");
            ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"),un);
            User user = serviceObj.GetResponseTemp<User>("AccountY?un="+un);
            return View(user);
        }
        //Need Update from here---------------------------------------------
        public ActionResult AddCourse1()
        {
            //StringCourse sc = new StringCourse();
            Course c=new Course();
            return View("AddCourse",c);
        }
        public ActionResult Create(Course c)
        {
            if (HttpContext.Session.GetString("token") == null) return Redirect("/Courses/Index");
            Trainer t = new Trainer();
            t.CourseId = c.ID;
            t.UserId = HttpContext.Session.GetString("user");
            c.Trainer.Add(t);
            StringCourse sc = c.ToStringCourse();
            return RedirectToAction("AddCourse2",sc);
        }
        //[ValidateAntiForgeryToken]
        public ActionResult AddCourse2(StringCourse sc)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //sc.StringDateToJSON();
                if(HttpContext.Session.GetString("token")==null) return Redirect("/Courses/Index");

                ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), HttpContext.Session.GetString("user"));
                serviceObj.PostResponse("Courses", sc);
                TempData["toastmsg"] = "Course Added Successfully!!!";
                //}
            }
            catch (Exception exp)
            {
                string g = exp.Message;
                TempData["toastmsg"] = "Course Addition Failed due to :"+g;
                return Redirect("/Courses/Index");
            }
            return Redirect("/Courses/Index");
        }

        public ActionResult Delete()
        {
            return View();
        }
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(FormCollection f)
        public ActionResult Delete2(int id)
        {
            //Session["ErrMsg_Re"] = null;
            //var id = Convert.ToInt32(f["ID"]);
            try
            {
                if (ModelState.IsValid)
                {
                    if(HttpContext.Session.GetString("token")==null) return Redirect("/Courses/Index");
                    ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), HttpContext.Session.GetString("user"));
                    serviceObj.DeleteResponse("Courses?id=" + id.ToString());
                    TempData["toastmsg"] = "Course Deleted Successfully!!!";
                }
                else
                {
                    TempData["toastmsg"] = "Course Deletion Failed due to invalid model state";
                    return Redirect("/Courses/Index");
                    //Session["ErrMsg_Re"] = "Id not exist";
                    //return Redirect("/Courses/Remove");
                }
            }
            catch (Exception exp)
            {
                string s = exp.Message;
                TempData["toastmsg"] = "Course Deletion Failed due to "+s;
                return Redirect("/Courses/Index");
                //return Redirect("/Courses/Remove");
            }

            //return Redirect("/Courses/Remove");
            return Redirect("/Courses/Index");
        }

        
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(FormCollection f)
        public ActionResult Edit(int id)
        {
            //Session["ErrMsg_Up"] = null;
            //int id = Convert.ToInt32(f["Id1"]);
            try
            {
                if(HttpContext.Session.GetString("token")==null) return Redirect("/Courses/Index");
                ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), HttpContext.Session.GetString("user"));
                Course v = serviceObj.GetOneResponse<Course>("Courses/" + id.ToString());//"Courses?id=" + id.ToString()
                return View(v);
            }
            catch (Exception exp)
            {
                string s = exp.Message;
                TempData["toastmsg"] = "Course Deletion Failed due to " + s;
                return Redirect("/Courses/Index");
            }

        }
        public ActionResult Update(Course c)
        {
            StringCourse sc = c.ToStringCourse();
            return RedirectToAction("UpdateEnd", sc);
        }
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateEnd(StringCourse t)
        {
            try
            {
                if (HttpContext.Session.GetString("token")==null) return Redirect("/Courses/Index");
                ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), HttpContext.Session.GetString("user"));
                serviceObj.PutResponse("Courses?id=" + t.ID.ToString(), t);
                TempData["toastmsg"] = "Course Edited Successfully!!!";
            }
            catch (Exception exp)
            {
                string s = exp.Message;
                TempData["toastmsg"] = "Course Deletion Failed due to " + s;
                return Redirect("/Courses/Index");
            }
            //return RedirectToAction("Update");
            return Redirect("/Courses/Index");
        }
    }
}
