using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using exam.Models;
using exam.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    
    [Route("api/exam")]
    public class ExamController : Controller
    {
        private readonly IExamRepository _exam;
        private readonly IQuestionRepository _question;
        public ExamController(IExamRepository exam,IQuestionRepository question)
        {
            _exam = exam;
            _question = question;
        }

        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> LExam()
        {   
            var exams = await _exam.getExam();
            return Ok(exams);
        }

        [Authorize(Roles = "1,2")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(string name,int duration, string strQuestions)
        {
            var lQuestion = strQuestions.Split(",");
            var listId = new int[lQuestion.Length];
            for (int i = 0; i < lQuestion.Length; i++)
            {
                if (!Int32.TryParse(lQuestion[i], out listId[i]))
                {
                    return StatusCode(500, "Error");
                }
            }
            await _exam.CreateExam(name, duration, listId);
            return Ok(new
            {
                msg = "Added!"
            });
        }
        [Authorize]
        [HttpPost]
        [Route("join")]
        public async Task<IActionResult> JoinExam(int id)
        {
            var LQuestion = await _question.GetExamQuestion(id);
            var exam = await _exam.Get(id);
            return Ok(new
            {
                name = exam.name,
                duration = exam.duration,
                questions = LQuestion
            });
        }

        [Authorize]
        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> Submit(int id,string strLAnswer)
        {
            var point = await _exam.Submit(id, strLAnswer);
            return Ok(new { point = point });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,string name,int duration,string strQuestions)
        {
            var listQuestions = strQuestions.Split(",");
            var listId = new int[listQuestions.Length];
            for (int i = 0; i < listQuestions.Length; i++)
            {
                if (!Int32.TryParse(listQuestions[i], out listId[i]))
                {
                    return StatusCode(500, "Error");
                }
            }
            await _exam.UpdateExam(id, name, duration, listId);
            return Ok(new
            {
                msg = "Updated!"
            });
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _exam.Delete(id);
            return Ok(new
            {
                msg = "Deleted!"
            });
        }
    }
}
