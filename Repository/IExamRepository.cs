using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using exam.Models;

namespace exam.Repository
{
    public interface IExamRepository : IBaseRepository<Exam> 
    {
        Task<List<Exam>> getExam();
        Exam SearchByKeyword();
        Task UpdateExam(int id,string name, int duration,int[] lQuestionId);
        Task CreateExam(string name, int duration, int[] lQuestionId);
        Task<List<Exam>> Search(string keyword,int perPage, int page);
        Task<int> Submit(int id, string strListAnswer);
    }
}
