using System.ComponentModel.DataAnnotations;

namespace LearningHubMVC.Models
{

    public class ShownUser
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string ID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Registered Date")]
        public DateTime registeredDate { get; set; }
        [Required]
        public Role Role { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public ShownUser()
        {
            Enrollments = new HashSet<Enrollment>();
        }

    }
    
}
