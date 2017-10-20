using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using exam.Models;
using exam.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Authorize(Roles = "2")]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        public ICategoryRepository _category { get; set; }

        public CategoryController(ICategoryRepository category)
        {
            _category = category;
        }

        [HttpGet("list")]
        public async Task<IActionResult> LCategory()
        {
            
            var categories = await _category.getAll();
            return Ok(categories);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(string name)
        {
            var c = new Category { name = name };
            await _category.Create(c);
            return Ok(new
            {
                msg = "Added!!!",
                id = c.id
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (name == null)
            {
                return BadRequest("Name is requried!");
            }
            var cate = await _category.Get(id);
            if (cate == null)
            {
                return NotFound();
            }
            else
            {
                cate.name = name;
                await _category.Update(id, cate);
                return Ok(new
                {
                    msg = "Updated!!!",
                    category = cate
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _category.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            await _category.Delete(id);
            return Ok(new { msg = "Deleted! " });

        }
    }
}