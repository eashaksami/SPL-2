using EBET.Models;
using EBET.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EBET.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionStatus>().HasKey(l => new { l.UserId, l.QuestionId });
        }
        
        public DbSet<Question> Questions { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PracticeExam> PracticeExams { get; set; }
        public DbSet<QuestionStatus> QuestionStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<TestExam> TestExams { get; set; }
        public DbQuery<NewGetQuestionDto> QuestionModel { get; set; }
        // public DbQuery<TestQuestionDto> TestQuestionModel { get; set; }
        public DbQuery<CourseDto> CoursesModel { get; set; }
        public DbQuery<SubscribedCourseDto> CourseModel { get; set; }
        // public DbQuery<CourseDto> Course { get; set; }
    }
} 
