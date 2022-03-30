using LearningHubApi2.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LearningHubApi2.Data_Layer
{
    public class LearningHubApiDbContext : DbContext
    {
        public LearningHubApiDbContext(DbContextOptions<LearningHubApiDbContext> options) : base(options)
        {

        }
        
        public DbSet<Course> Course { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Trainer> Trainer { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.ToTable("Trainer");
                entity.HasKey(p => new { p.CourseId, p.UserId });
                
                entity.HasOne(t => t.Course)
                .WithMany(p=>p.Trainer)
                .HasForeignKey(d => d.CourseId);
                entity.HasOne(t => t.User)
                .WithMany(p => p.Trainer)
                .HasForeignKey(d => d.UserId);
                

            });
            modelBuilder.Entity<Enrollment>( entity =>
            {
                entity.ToTable("Enrollment");
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseID);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.UserID);
            });
            

        }

    }
}
