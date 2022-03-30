using System.ComponentModel.DataAnnotations;

namespace LearningHubMVC.Models
{
    public enum Role
    {
        admin, trainer, student
    }

    public class User
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
        public virtual ICollection<Trainer> Trainer { get; set; }

        public User()
        {
            Enrollments = new HashSet<Enrollment>();
            Trainer = new HashSet<Trainer>();
        }

    }
}
