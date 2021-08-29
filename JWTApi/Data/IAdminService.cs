using System.Collections.Generic;
using System.Threading.Tasks;
using EBET.Dtos;
using EBET.Models;
using Microsoft.AspNetCore.Http;

namespace EBET.Data
{
    public interface IAdminService
    {
        Task<UploadQuestionDto> UploadQuestion(UploadQuestionDto uploadQuestionDto);
        Task<IEnumerable<GetQuestionDto>> ViewQuestions(int chapterId);
        Task<Question> UpdateQuestion(GetQuestionDto questionDto);
        Task<IEnumerable<UserForLoginDto>> ViewAllStudents();
        Course addNewCourse(string courseName, int imageId);
        Image uploadImage(IFormFile file);
        Task<Question> DeleteQuestion(int questionId);
    }
}
