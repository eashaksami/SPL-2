using System.Collections.Generic;
using System.Threading.Tasks;
using EBET.Dtos;
using EBET.Models;

namespace EBET.Data
{
    public interface IPerformanceService
    {
        Task<IEnumerable<TestExam>> GetProgressInfo(int studentId, int courseCode);
        Task<IEnumerable<CourseCompletionInfoDto>> GetCourseCompletionInfo(int studentId, int courseCode);
    }
}
