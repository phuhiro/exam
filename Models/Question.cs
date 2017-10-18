using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam.Models
{
    public class Question
    {
        public int id { get; set; }
        public string content { get; set; }
        [Column(TypeName = "TEXT")]
        public string answer1 { get; set; }
        [Column(TypeName = "TEXT")]
        public string answer2 { get; set; }
        [Column(TypeName = "TEXT")]
        public string answer3 { get; set; }
        [Column(TypeName = "TEXT")]
        public string answer4 { get; set; }
        public int correct_answer { get; set; }
        public virtual User user { get; set; }
    }
}
