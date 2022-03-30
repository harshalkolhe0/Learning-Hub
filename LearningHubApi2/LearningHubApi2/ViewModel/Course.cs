using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningHubApi2.ViewModel
{
    public class Course
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        public DateTime startdate { get; set; }
        [Required]
        public DateTime enddate { get; set; }
        public decimal? duration { get; set; }
        [StringLength(200)]
        public string? info { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Trainer> Trainer { get; set; }
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
            Trainer = new HashSet<Trainer>();
        }

    }
}
