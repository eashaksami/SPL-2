using System.Collections.Generic;
using System.Threading.Tasks;
using EBET.Models;
using EBET.Dtos;

namespace EBET.Data
{
    public interface ITestExamRepo
    {
        Task<IEnumerable<Chapter>> GetChapter(int id);
        Task<IEnumerable<GetQuestionDto>> GetQuestion(int studentId,string examType, int correctOrWrong,
                                        int seenOrUnseen, int totalQuestion, int[] chapterIds);
        // Task<IEnumerable<TestQuestionDto>> GetTestQuestion(int studentId,string examType, int correctOrWrong,
        //                                 int seenOrUnseen, int totalQuestion, int[] chapterIds);
        Task<IEnumerable<NewGetQuestionDto>> GetDemoQuestion(int CourseCode);
        Task<IEnumerable<CourseDto>> GetCourse(int studentId);
    }
}
