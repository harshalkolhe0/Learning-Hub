namespace LearningHubApi2.ViewModel
{
    public class UserTokens
    {
        public string Token
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public TimeSpan Validaty
        {
            get;
            set;
        }
        public string RefreshToken
        {
            get;
            set;
        }
        /*
        public Guid Id
        {
            get;
            set;
        }
        public string EmailId
        {
            get;
            set;
        }
        public Guid GuidId
        {
            get;
            set;
        }
        */
        public DateTime ExpiredTime
        {
            get;
            set;
        }
        public DateTime registeredDate { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
