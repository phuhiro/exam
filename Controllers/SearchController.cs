using System;
using System.Threading.Tasks;
using exam.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Authorize]
    [Route("api/search")]
    public class SearchController : Controller
    {
        private readonly IUserRepository _user;
        private readonly IExamRepository _exam;
        private readonly IQuestionRepository _question;
        private readonly ICategoryRepository _cate;
        public SearchController(IUserRepository user,IQuestionRepository question,
                                IExamRepository exam,ICategoryRepository cate)
        {
            _user = user;
            _exam = exam;
            _question = question;
            _cate = cate;
        }

        [HttpGet("user")]
        public async Task<IActionResult> SearchUser(string keyword)
        {
            var users = await _user.Search(keyword);
            return Ok(users);      
        }

        [HttpGet("exam")]
        public async Task<IActionResult> SearchExam(string keyword,int perPage = 15, int page = 1)
        {
            var exams = await _exam.Search(keyword, perPage, page);
            return Ok(exams);
        }

    }
}
