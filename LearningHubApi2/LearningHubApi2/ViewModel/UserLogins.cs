using System.ComponentModel.DataAnnotations;

namespace LearningHubApi2.ViewModel
{
    public class UserLogins
    {
        [Required]
        public string UserName
        {
            get;
            set;
        }
        [Required]
        public string Password
        {
            get;
            set;
        }

        public UserLogins() { }
    }
}
