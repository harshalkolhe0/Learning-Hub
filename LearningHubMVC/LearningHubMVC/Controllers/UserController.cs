using LearningHubMVC.Models;
using LearningHubMVC.ServiceRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace LearningHubMVC.Controllers
{
    public class UserController : Controller
    {
        public static List<ShownCourse>? scourses = new List<ShownCourse>();
        public static List<ShownUser>? susers = new List<ShownUser>();
        public static List<ShownCourse>? presentSCourses = new List<ShownCourse>();
        public static List<Enrollment>? myenrollments = new List<Enrollment>();
        public static List<Enrollment>? presentenrollments = new List<Enrollment>();
        public static HashSet<int>? courseId = new HashSet<int>();
        public static Dictionary<int, int>? courseenroll = new Dictionary<int, int>();
        public static List<User> traineranduser = new List<User>();
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Gettrainers()
        {
            try
            {
                if (HttpContext.Session.GetString("token") == null) return;
                if (HttpContext.Session.GetString("role") != "admin") return; 
                string un = HttpContext.Session.GetString("user");
                ServiceRepo serviceObj = new ServiceRepo(HttpContext.Session.GetString("token"), un);
                List<User> users = serviceObj.GetResponse<User>("Users");
                foreach (var item in users)
                {
                    if (item.Role.ToString() == "admin") continue;
                    traineranduser.Add(item);
                }
            }
            catch (Exception Exp)
            {
                String s = Exp.Message;
                throw;
            }
        }
        public IActionResult Index()
        {
            Gettrainers();
            return View(traineranduser);
        }
        public IActionResult LoginPage()
        {
            return View();
        }
        public IActionResult TryLogin(LoginUser user)
        {

            ServiceRepo serviceObj = new ServiceRepo("",user.username);
            string token=serviceObj.GetToken("AccountY",user);
            if(token.Contains("\""))
            {
                int n = token.Length;
                string ns = "";
                for (int i = 1; i < n - 1; i++)
                {
                    ns += token[i];
                    //Console.WriteLine(token[i]);
                }
                token = ns;
            }
            if (token == null)
            {
                TempData["msg"] = token;
                return RedirectToAction("LoginPage");
            }
            if (token.Length == 0)
            {
                TempData["msg"] = token;
                return RedirectToAction("LoginPage");
            }
            HttpContext.Session.SetString("token", token);
            HttpContext.Session.SetString("user", user.username);
            string role = getRole(token);
            HttpContext.Session.SetString("role", role);
            return Redirect("/Courses/Index/");
            //return Redirect("/Courses/GetValues/");
        }
        
        public string getRole(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            
            //_configuration is null find it first
            var key = Convert.FromBase64String(_configuration["Jwt:Key"].ToString());
            int j = 10;
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                //ValidIssuer = _configuration["Jwt:ValidIssuer"],
                //ValidAudience = _configuration["Jwt:ValidAudience"],
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                //ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            //var Id = jwtToken.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            //get the user name and role from the JWT token.
            // username = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            
            var role = jwtToken.Claims.FirstOrDefault(x => x.Type == "role")?.Value;
            return role.ToString();
        }
    }
}
