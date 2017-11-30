using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUserRepository _user;
        public ExamController(IExamRepository exam, IQuestionRepository question,
                              IUserRepository user)
        {
            _exam = exam;
            _question = question;
            _user = user;
        }

        [Authorize(Roles = "1,2")]
        [HttpGet("list")]
        public async Task<IActionResult> LExam()
        {
            var exams = await _exam.getExam();
            return Ok(exams);
        }

        [Authorize]
        [HttpGet("listbyteacher")]
        public async Task<IActionResult> LTExam()
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == "userid").Value;
            var exams = await _exam.getByTeacher(Int16.Parse(userid));
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getExam(int id)
        {

            var exam = await _exam.Get(id);
            if (exam == null) return NotFound();
            return Ok(exam);
        }
        [Authorize(Roles = "1,2")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(string name, string description
                                                , int duration,
                                                List<Question> questions,
                                               int cateid = 1)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == "userid").Value;
            User u = await _user.Get(Int16.Parse(userid));
            await _exam.CreateExam(name, description, duration, questions, u, cateid);
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
                description = exam.description,
                cateid = exam.cateid,
                duration = exam.duration,
                questions = LQuestion
            });
        }

        [Authorize]
        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> Submit(int id, string strLAnswer)
        {
            var point = await _exam.Submit(id, strLAnswer);
            return Ok(new { point = point });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name, string description
                                                , int duration,
                                                List<Question> questions,
                                               int cateid = 1)
        {

            await _exam.UpdateExam(id, name, description, duration, questions, cateid);
            return Ok(new
            {
                msg = "Added!"
            });

        }

        [Authorize]
        [HttpDelete("{id}")]
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
