using System;
namespace exam.Models
{
    public class PointExam
    {
        public int id { get; set; }
        public virtual User user { get; set; }
        public virtual Exam exam { get; set; }
        public int point { get; set; }
    }
}
