using System;
namespace exam.Models
{
    public class ExamQuestion
    {
        public int id { get; set; }
        public int questionId { get; set; }
        public int examId { get; set; }
        public virtual Question question { get; set; }
        public virtual Exam exam { get; set; }
    }
}