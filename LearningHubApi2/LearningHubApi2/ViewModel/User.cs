using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningHubApi2.ViewModel
{
    public enum Role
    {
        admin,trainer,student
    }

    public class User
    {
        [Key]
        [Required]
        [StringLength(100)]
        public string ID { get; set; }
        [Required]
        [StringLength(100)]
        public string password { get; set; }
        [Required]
        public DateTime registeredDate { get; set; }
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
