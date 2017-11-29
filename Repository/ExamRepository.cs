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
                                 .Include(e => e.user)
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

        public async Task CreateExam(string name, string description,
                                     int duration, List<Question> questions,
                                    User u,int cateid)
        {
            var nLExamQuestion = new List<ExamQuestion>();
            foreach (Question q in questions)
            {
               
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
                description = description,
                examquestions = nLExamQuestion,
                user = u,
                cateid = cateid
            };
            await _context.exams.AddAsync(ex);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Updates the exam.
        /// </summary>
        /// <returns>The exam.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="duration">Duration.</param>
        public async Task UpdateExam(int id, string name, string description ,
                                     int duration, List<Question> questions,
                                    int cateid)
        {
            var exam = await _context.exams
                                     .Include(e => e.examquestions)
                                     .Include(e => e.user)
                                     .FirstAsync(e => e.id == id);
            if(exam != null)
            {
                exam.name = name.Equals("") ? exam.name : name;
                exam.duration = duration == 0 ? exam.duration : duration;
                var nLExamQuestion = new List<ExamQuestion>();
                foreach (Question q in questions)
                {

                    if (q != null)
                    {
                        if(q.id == 0){
                            var tmp = q;
                            await _context.questions.AddAsync(tmp);
                            await _context.SaveChangesAsync();
                            nLExamQuestion.Add(new ExamQuestion{exam = exam,question = tmp});
                        } else {
                            var questionTmp = await _context.questions.FindAsync(q.id);
                            questionTmp.correct_answer = q.correct_answer;
                            questionTmp.answer1 = q.answer1;
                            questionTmp.answer2 = q.answer2;
                            questionTmp.answer3 = q.answer3;
                            questionTmp.answer4 = q.answer4;
                            await _context.SaveChangesAsync();
                            nLExamQuestion.Add(new ExamQuestion{exam = exam,question = questionTmp});
                        }

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
            int correct = 0;
            var lAnswer = strListAnswer.Split(",");
            var questions = await _context.questions.Where(q => q.examquestions.Any(eq => eq.examId == id))
                                       .ToListAsync();
            for (int i = 0; i < lAnswer.Length; i++)
            {
                if (Int32.Parse(lAnswer[i]) == questions[i].correct_answer) correct++;
            }
            var point = (int)((float)correct / questions.Count() * 100);
            return point;
        }

        /// <summary>
        /// Gets the exam by catgory.
        /// </summary>
        /// <returns>The exam by catgory.</returns>
        /// <param name="cateId">Cate identifier.</param>
        /// <param name="perpage">Perpage.</param>
        /// <param name="page">Page.</param>
        public async Task<List<Exam>> getExamByCatgory(int cateId,int perpage = 0, int page = 1)
        {

            if(perpage == 0 ) {
                var exams = await _context.exams.Where(e => e.cateid == cateId)
                                          .Include(e => e.user)
                                          .Select(e => new Exam{
                    id = e.id,
                    name = e.name,
                    duration = e.duration,
                    user = new User{username = e.user.username,name = e.user.name }
                                           })
                                          .ToListAsync();
                return exams;
            } else {
                var exams = await _context.exams.Where(e => e.cateid == cateId)
                                          .Include(e => e.user)
                                          .Take(perpage)
                                          .Skip((page - 1) * perpage)
                                          .ToListAsync();
                return exams;
            }
        }

        public async Task<List<Exam>> getByTeacher(int teacherId)
        {
            var u = await _context.users.FindAsync(teacherId);
            var exams = await _context.exams.Where(e => e.user == u)
                                      .ToListAsync();
            return exams;
        }
    }
}
