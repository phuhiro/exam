using System.Collections.Generic;
using System.Threading.Tasks;
using exam.Models;
using exam.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Authorize(Roles = "1,2")]
    [Route("api/question")]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _question;
        private readonly IUserRepository _user;
        public QuestionController(IQuestionRepository question, IUserRepository user)
        {
            _question = question;
            _user = user;
        }

        [HttpGet("list")]
        public async Task<IActionResult> list(int perpage,int page = 1)
        {
            var questions = new List<Question>();
            if(perpage == 0){
                questions = await _question.getAll();
            } else {
               questions = await _question.paginate(perpage, page);
            }

            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = await _question.Get(id);
            return Ok(question);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] string content,
                                               string answer1, string answer2, string answer3,
                                                string answer4, int correct_answer)
        {

            var user = await _user.Get(1);
            var q = new Question
            {
                content = content,
                answer1 = answer1,
                answer2 = answer2,
                answer3 = answer3,
                answer4 = answer4,
                correct_answer = correct_answer,
                user = user
            };
            await _question.Create(q);
            return Ok(q);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string content,
                                               string answer1,string answer2,string answer3,
                                                string answer4,int correct_answer)
        {
            
            var item = await _question.Get(id);
            if (item == null) return NotFound();
            item.content = item.content != null ? content : item.content;
            item.answer1 = item.answer1 != null ? answer1 : item.answer1;
            item.answer2 = item.answer2 != null ? answer2 : item.answer2;
            item.answer3 = item.answer3 != null ? answer3 : item.answer3;
            item.answer4 = item.answer4 != null ? answer4 : item.answer4;
            item.correct_answer = item.correct_answer != 0 ? correct_answer : item.correct_answer;
            await _question.Update(id, item);
            return Ok(new {
                msg = "Updated!",
                question = item
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _question.Get(id);
            if (item == null) return NotFound();
            await _question.Delete(id);
            return Ok(new {
                msg = "Deleted!"
            });
        }
    }
}
