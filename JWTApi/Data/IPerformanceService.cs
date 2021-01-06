using System.Collections.Generic;
using System.Threading.Tasks;
using JWTApi.Dtos;
using JWTApi.Models;

namespace JWTApi.Data
{
    public interface IPerformanceService
    {
        Task<IEnumerable<TestExam>> GetProgressInfo(int courseCode, int studentId);
        Task<IEnumerable<CourseCompletionInfoDto>> GetCourseCompletionInfo(int courseCode, int studentId);
    }
}