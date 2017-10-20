using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace exam.Models
{
    public class Exam
    {
        public int id { get; set; }
        [MaxLength(200)]
        public string name { get; set; }
        public int cateid { get; set; }
        public virtual Category cate { get; set; }
        public int duration { get; set; }
        public virtual List<ExamQuestion> examquestions { get; set; }
    }
}