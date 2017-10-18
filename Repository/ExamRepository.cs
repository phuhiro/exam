using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam.Models;

namespace exam.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext _context;
        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public Task Create(Exam o)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Exam> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Exam>> getAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exam> SearchByKeyword()
        {
            throw new NotImplementedException();
        }

        public Task Update(int primary, Exam o)
        {
            throw new NotImplementedException();
        }
    }
}
