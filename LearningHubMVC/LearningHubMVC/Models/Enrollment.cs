namespace LearningHubMVC.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public string UserID { get; set; }
        public int CourseID { get; set; }
        public DateTime enrollmentDate { get; set; }
        public virtual Course? Course { get; set; }
        public virtual User? User { get; set; }
    }
}
