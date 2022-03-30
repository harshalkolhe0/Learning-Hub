using System.ComponentModel.DataAnnotations;

namespace LearningHubMVC.Models
{
    public class Course
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime startdate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime enddate { get; set; }
        [Display(Name = "Duration")]
        public decimal? duration { get; set; }
        [Display(Name = "Description")]
        public string? info { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Trainer> Trainer { get; set; }
        public Course()
        {
            info = "Enter Your Description";
            Name = "";
            duration = (decimal?)0.00;
            startdate = DateTime.Now;
            enddate =DateTime.Now;
            Enrollments = new HashSet<Enrollment>();
            Trainer = new HashSet<Trainer>();
        }
        public StringCourse ToStringCourse()
        {
            StringCourse c = new StringCourse();
            c.ID = ID;
            c.Name = Name;
            c.info = info;
            c.duration = duration;
            c.Enrollments = null;
            c.Trainer = Trainer;
            c.startdate = string.Format("{0:dd/MM/yyyy hh:mm tt}", startdate);
            c.enddate = string.Format("{0:dd/MM/yyyy hh:mm tt}", enddate);
            return c;
        }

        public ShownCourse ToShownCourse(bool b,int? val,bool own)
        {
            ShownCourse sc = new ShownCourse();
            sc.ID = ID;
            sc.Name = Name;
            sc.info = info;
            sc.duration = duration;
            sc.Enrollments = null;
            sc.Trainer = Trainer;
            sc.startdate = startdate;
            sc.enddate = enddate;
            sc.isenrolled = b;
            sc.istrainer = own;
            sc.enrollemntId = val;
            return sc;
        }
    }
}
