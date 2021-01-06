using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTApi.Dtos;
using JWTApi.Models;
using Microsoft.EntityFrameworkCore;
namespace JWTApi.Data
{
    public class PerformanceService : IPerformanceService
    {
        private readonly DataContext _context;

        public PerformanceService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseCompletionInfoDto>> GetCourseCompletionInfo(int courseCode, int studentId)
        {
            var totalQsn =  ((from p in _context.Questions.AsEnumerable()
                      join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
                      where (q.StudentId == studentId && q.CourseCode == courseCode)
                      group new { p, q } by new { p.ChapterId } into g
                      select new CourseCompletionInfo { ChapterId = g.Key.ChapterId, TotalQuestion = g.Count() }).ToList());
            
            var totalSeen = ((from p in _context.Questions.AsEnumerable()
                      join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
                      where (q.TSeenOrUnseen == 1 && q.StudentId == studentId && q.CourseCode == courseCode)
                      group new { p, q } by new { p.ChapterId } into g
                      select new CourseCompletionInfo { ChapterId = g.Key.ChapterId, TotalQuestion = g.Count() }).ToList());
            
            var courses = _context.Chapters.FromSqlRaw("SELECT DISTINCT Chapters.ChapterId, Chapters.Name, Chapters.CourseCOde FROM Questions, Chapters WHERE Chapters.ChapterId IN(SELECT DISTINCT ChapterId FROM Questions) AND Chapters.CourseCode = {0}", courseCode).ToList();

            IEnumerable<CourseCompletionInfoDto> info = new List<CourseCompletionInfoDto>();
            int len = totalSeen.Count();
            if(len != 0)
            {
                int j = 0, k = 0;
                foreach(var i in totalQsn){
                    int totalSeenQuestion = 0;
                    if(i.ChapterId != totalSeen[j].ChapterId) totalSeenQuestion = 0;
                    else
                    {
                        totalSeenQuestion = totalSeen[j].TotalQuestion;
                        j++;
                    } 
                    var z = new CourseCompletionInfoDto
                    {
                        Name = courses[k].Name, 
                        TotalSeen = totalSeenQuestion, 
                        totalQuestion = i.TotalQuestion
                    };
                    info = info.Append(z);
                    k++;
                }
            }

            return await Task.FromResult(info);
        }

        public async Task<IEnumerable<TestExam>> GetProgressInfo(int courseCode, int studentId)
        {
            var info = await _context.TestExams
                    .FromSqlRaw("SELECT TestExamId, Quantity, TotalCorrectAnswer, TotalWrongAnswer, StudentId, CourseCode FROM TestExams WHERE StudentId = {0} AND CourseCode = {1}", studentId, courseCode).ToListAsync();
            return info;
        }
    }
}

// https://www.youtube.com/watch?v=dRVdxlwxIK0