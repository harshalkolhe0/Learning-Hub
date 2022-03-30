using LearningHubApi2.Data_Layer;
using LearningHubApi2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace LearningHubApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountYController : ControllerBase
    {
        private readonly LearningHubApiDbContext _context;
        public AccountYController(LearningHubApiDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public IActionResult ValidLogin(UserLogins user)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = _context.User.Any(x => x.ID.Equals(user.UserName) && x.password.Equals(user.Password));//, StringComparison.OrdinalIgnoreCase));
                if(Valid)
                {
                    var acc = _context.User.FirstOrDefault(x => x.ID.Equals(user.UserName));
                    string val = TokenManager.GenerateToken(acc.ID, acc.Role.ToString());
                    
                    //HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, val);
                    return Ok(val);
                }
                else
                {
                    
                    //return Request.CreateErrorResponse(HttpStatusCode.BadGateway,
                    //   "username or password is incorrect");
                    return BadRequest("username or password is incorrect");
                }


            }
            catch (Exception ex)
            {
                string s=ex.Message;
                return BadRequest("username or password is incorrect");
                //return Request.CreateErrorResponse(HttpStatusCode.BadGateway,
                //        "username or password is incorrect");
            }
        }

        [TokenAuthenticationFilter]
        [HttpGet]
        
        public IActionResult GetUser()
        {
            //string un = "hk";
            //var acc = _context.User.FirstOrDefault(x => x.ID.Equals(un));
            string acc = "Successfull";
            return Ok(acc);
        }

    }
}
