using LearningHubApi2.Data_Layer;
using LearningHubApi2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningHubApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly LearningHubApiDbContext _context;
        private readonly JwtSettings jwtSettings;
        public AccountController(LearningHubApiDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            this.jwtSettings = jwtSettings;
        }
        //GetToken - ValidLogin
        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = _context.User.Any(x => x.ID.Equals(userLogins.UserName) && x.password.Equals(userLogins.Password));//, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = _context.User.FirstOrDefault(x => x.ID.Equals(userLogins.UserName));// StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        UserName = user.ID,
                        Role = user.Role, 
                    }, jwtSettings);
                }
                else
                {
                    return BadRequest($"Invalid username or password");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid username or password");
            }
        }
        /// <summary>
        /// Get List of UserAccounts
        /// </summary>
        /// <returns>List Of UserAccounts</returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetList()
        {
            return Ok(_context.User);
        }
    }
}
