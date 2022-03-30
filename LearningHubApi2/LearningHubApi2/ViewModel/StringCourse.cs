using System.Globalization;

namespace LearningHubApi2.ViewModel
{
    public class StringCourse
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public decimal? duration { get; set; }
        public string? info { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public StringCourse()
        {
            Enrollments = new HashSet<Enrollment>();
        }
        public void StringDateToJSON()
        {
            DateTime d = DateTime.ParseExact(startdate, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            startdate = string.Format("{0:s}", d);
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
            Course c = new Course();
            c.ID = ID;
            c.Name = Name;
            c.info = info;
            c.duration = duration;
            c.Enrollments = null;
            c.startdate = DateTime.ParseExact(startdate, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            c.enddate = DateTime.ParseExact(enddate, "dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture);
            return c;
        }
    }
}
