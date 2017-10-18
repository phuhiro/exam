using System;
namespace exam.Models
{
    public class ExamQuestion
    {
        public int id { get; set; }
        public virtual Question question { get; set; }
        public virtual Exam exam { get; set; }
    }
}