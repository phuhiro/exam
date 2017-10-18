using System;
using System.Collections.Generic;
using exam.Models;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Route("api/exam")]
    public class ExamController : Controller
    {
        public ExamController()
        {  
            
        }

        [HttpGet("list")]
        public List<Exam> LExam([FromBody] int perpage = 20,int page = 1)
        {
            
            return null;
        }

    }
}
