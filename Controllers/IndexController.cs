using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam.Controllers
{
    [Route("api/index")]
    public class IndexController : Controller
    {
        private readonly ICategoryRepository _category;
        private readonly IExamRepository _exam;
        public IndexController(ICategoryRepository category, IExamRepository exam)
        {
            _category = category;
            _exam = exam;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Object> cateexams = new List<object>();
            var categories = await _category.getAll();
            foreach (var category in categories)
            {
                var exams = await _exam.getExamByCatgory(category.id);
                var tmp = new { name = category.name, exams = exams };
                cateexams.Add(tmp);

            }

            return Ok(cateexams);
        }
    }

 }