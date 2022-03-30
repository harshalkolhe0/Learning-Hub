using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningHubApi2.ViewModel
{
    public class Enrollment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentID { get; set; }
        [Required]
        [StringLength(100)]
        public string UserID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public DateTime enrollmentDate { get; set; }

        public virtual Course? Course { get; set; }
        public virtual User? User { get; set; }

    }
}
