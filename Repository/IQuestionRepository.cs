using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using exam.Models;

namespace exam.Repository
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
       Task<List<Question>> GetExamQuestion(int examId);
    }

}
