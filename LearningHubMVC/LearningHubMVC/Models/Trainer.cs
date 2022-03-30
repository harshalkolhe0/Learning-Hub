using System.Diagnostics.CodeAnalysis;

namespace LearningHubMVC.Models
{
    public class Trainer : IEquatable<Trainer>
    {
        public int CourseId { get; set; }

        public Course? Course { get; set; }

        public string UserId { get; set; }

        public User? User { get; set; }
        public Trainer()
        {

        }
        public Trainer(string uid,int id)
        {
            UserId = uid;
            CourseId = id;
        }
        public override int GetHashCode()
        {
            return UserId.GetHashCode(); // Or something like that
        }

        public override bool Equals(object obj)
        {
            return obj is Trainer && Equals((Trainer)obj);
        }

        public bool Equals(Trainer? other)
        {
            return (other.UserId == UserId) && (other.CourseId == CourseId);
        }
    }
    public class TrainerComp : IEqualityComparer<Trainer>
    {
        public bool Equals(Trainer? x, Trainer? y)
        {
            return (x.UserId == y.UserId) && (x.CourseId == y.CourseId);
        }

        public int GetHashCode([DisallowNull] Trainer obj)
        {
            return obj.UserId.GetHashCode();
        }
    }
}
