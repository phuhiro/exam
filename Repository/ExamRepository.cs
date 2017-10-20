using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam.Models;
using Microsoft.EntityFrameworkCore;

namespace exam.Repository
{
    public class ExamRepository : Repository<Exam>,IExamRepository
    {
        public ExamRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the exam with cate
        /// </summary>
        /// <returns>The exam with cate</returns>
        public async Task<List<Exam>> getExam()
        {
            return await _context.exams.Include(e => e.cate)
                                 .ToListAsync();
        }
        /// <summary>
        /// Searchs the by keyword.
        /// </summary>
        /// <returns>The by keyword.</returns>
        public Exam SearchByKeyword()
        {
            return null;
            throw new NotImplementedException();
        }

        public async Task CreateExam(string name, int duration, int[] lQuestionId)
        {
            var nLExamQuestion = new List<ExamQuestion>();
            foreach (int qId in lQuestionId)
            {
                var q = await _context.questions.FindAsync(qId);
                if (q != null)
                {
                    nLExamQuestion.Add(new ExamQuestion
                    {
                        question = q
                    });
                }
            }
            var ex = new Exam{
                name = name,
                duration = duration,
                examquestions = nLExamQuestion
            };
            await _context.exams.AddAsync(ex);
        }

        /// <summary>
        /// Updates the exam.
        /// </summary>
        /// <returns>The exam.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="duration">Duration.</param>
        /// <param name="lQuestionId">List Question ID.</param>
        public async Task UpdateExam(int id, string name, int duration, int[] lQuestionId)
        {
            var exam = await _context.exams
                                     .Include(e => e.examquestions)
                                     .FirstAsync(e => e.id == id);
            if(exam != null)
            {
                exam.name = name.Equals("") ? exam.name : name;
                exam.duration = duration == 0 ? exam.duration : duration;
                var nLExamQuestion = new List<ExamQuestion>();
                foreach(int qId in lQuestionId)
                {
                    var q = await _context.questions.FindAsync(qId);
                    if(q != null){
                        nLExamQuestion.Add(new ExamQuestion{
                            exam = exam,
                            question = q
                        });                     
                    }
                }
                exam.examquestions = nLExamQuestion;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Exam>> Search(string keyword,int perPage = 10,int page = 1)
        {
            var exams = await _context.exams.Where(ex => ex.name.Contains(keyword))
                                .Take(perPage)
                                .Skip((page - 1) * perPage)
                                .ToListAsync();
            return exams;
        }

        public async Task<int> Submit(int id,string strListAnswer)
        {
            int point = 0;
            var lAnswer = strListAnswer.Split(",");
            var questions = await _context.questions.Where(q => q.examquestions.Any(eq => eq.examId == id))
                                       .ToListAsync();
            for (int i = 0; i < lAnswer.Length; i++)
            {
                if (Int32.Parse(lAnswer[i]) == questions[i].correct_answer) point++;
            }
            return point;
        }
    }
}
