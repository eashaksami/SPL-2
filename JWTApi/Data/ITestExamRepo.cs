using System.Collections.Generic;
using System.Threading.Tasks;
using JWTApi.Models;
using JWTApi.Dtos;

namespace JWTApi.Data
{
    public interface ITestExamRepo
    {
        Task<IEnumerable<Chapter>> GetChapter(int id);
        Task<IEnumerable<GetQuestionDto>> GetQuestion(int studentId,string examType, int correctOrWrong,
                                        int seenOrUnseen, int totalQuestion, int[] chapterIds);
        Task<IEnumerable<TestQuestionDto>> GetTestQuestion(int studentId,string examType, int correctOrWrong,
                                        int seenOrUnseen, int totalQuestion, int[] chapterIds);
        Task<IEnumerable<GetQuestionDto>> GetDemoQuestion(int CourseCode);
        Task<IEnumerable<CourseDto>> GetCourse(int studentId);
    }
}