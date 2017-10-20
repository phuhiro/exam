using System;
using Microsoft.EntityFrameworkCore;

namespace exam.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt)
            : base(opt)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Exam> exams { get; set; }
        public DbSet<ExamQuestion> exam_questions { get; set; }
        public DbSet<PointExam> point_exams { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
