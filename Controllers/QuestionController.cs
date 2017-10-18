using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using exam.Models;
using exam.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Route("api/question")]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _question;
        public QuestionController(IQuestionRepository question)
        {
            _question = question;
        }

        [HttpGet("list")]
        public async Task<IActionResult> list()
        {
            var questions = await _question.getAll();
            return Ok(questions);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> create([FromBody] Question q)
        {
            q.id = 0;
            await _question.Create(q);
            return Ok(q);
        }
    }
}
