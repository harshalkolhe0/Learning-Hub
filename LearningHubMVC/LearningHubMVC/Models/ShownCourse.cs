namespace LearningHubMVC.Models
{
    public class ShownCourse 
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        
        public DateTime startdate { get; set; }
        
        public DateTime enddate { get; set; }
        
        public decimal? duration { get; set; }
        
        public string? info { get; set; }
        public bool? isenrolled { get; set; }= false;
        public bool? istrainer { get; set; } = false;
        public int? enrollemntId { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Trainer> Trainer { get; set; }

        
    }
}
