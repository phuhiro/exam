using System;
using System.Collections.Generic;
using exam.Models;

namespace exam.Repository
{
    public interface IExamRepository : IBaseRepository<Exam> 
    {
        IEnumerable<Exam> SearchByKeyword();

    }
}
