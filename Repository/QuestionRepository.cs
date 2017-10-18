using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using exam.Models;
using Microsoft.EntityFrameworkCore;

namespace exam.Repository
{
    public class QuestionRepository : Repository<Question>,IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context){}
    }
}
