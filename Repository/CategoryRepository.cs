using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam.Models;
using Microsoft.EntityFrameworkCore;

namespace exam.Repository
{
    public class CategoryRepository :Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
