using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using exam.Models;

namespace exam.Repository
{
    public interface IExamRepository : IBaseRepository<Exam> 
    {
        Task<List<Exam>> getExam();
        Task<List<Exam>> getExamByCatgory(int cateId,int perpage = 0,int page = 1);
        Exam SearchByKeyword();
        Task UpdateExam(int id,string name, string description ,int duration,
                        List<Question> questions,int cateid);
        Task CreateExam(string name, string description,
                        int duration, List<Question> questions,
                       User u,int cateid);
        Task<List<Exam>> Search(string keyword,int perPage, int page);
        Task<int> Submit(int id, string strListAnswer);
        Task<List<Exam>> getByTeacher(int teacherId);
    }
}
