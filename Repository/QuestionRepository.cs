using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam.Models;
using Microsoft.EntityFrameworkCore;

namespace exam.Repository
{
    public class QuestionRepository : Repository<Question>,IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context){}

        public async Task<List<Question>> GetExamQuestion(int examId)
        {
            return await _context.questions.Where(q => q.examquestions.Any( eq => eq.examId == examId))
                                       .ToListAsync();
        }

        public ApplicationDbContext inerContext
        {
            get { return _context as ApplicationDbContext; }
        }
    }
}
