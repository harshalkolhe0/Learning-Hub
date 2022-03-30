using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LearningHubMVC.Models
{
    public class StringCourse
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public string startdate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public string enddate { get; set; }
        [Required]
        [Display(Name = "Duration")]
        public decimal? duration { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string? info { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Trainer> Trainer { get; set; }
        public StringCourse()
        {
            info = "Enter Your Description";
            Name = "";
            duration = (decimal?)0.00;
            startdate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            enddate = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            Enrollments = new HashSet<Enrollment>();
            Trainer = new HashSet<Trainer>();
        }
        public void StringDateToJSON()
        {
            DateTime d= DateTime.ParseExact(startdate, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            startdate=string.Format("{0:s}", d);
            d = DateTime.ParseExact(enddate, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            enddate = string.Format("{0:s}", d);
        }
        public void JSONToStringDate()
        {
            DateTime d = DateTime.ParseExact(startdate, "{0:s}", CultureInfo.InvariantCulture);
            startdate = string.Format("{0:dd/MM/yyyy hh:mm tt}", d);
            d = DateTime.ParseExact(enddate, "{0:s}", CultureInfo.InvariantCulture);
            enddate = string.Format("{0:dd/MM/yyyy hh:mm tt}", d);
        }
        public Course ToCourse()
        {
            Course c=new Course();
            c.ID = ID;
            c.Name= Name;
            c.info= info;
            c.duration= duration;
            c.Enrollments = null;
            c.Trainer = Trainer;
            c.startdate = DateTime.ParseExact(startdate, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            c.enddate = DateTime.ParseExact(enddate, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            return c;
        }
    }
}
