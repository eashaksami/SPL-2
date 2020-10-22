using System.Collections.Generic;
using System.Threading.Tasks;
using JWTApi.Dtos;

namespace JWTApi.Data
{
    public interface IAdminService
    {
        Task<UploadQuestionDto> UploadQuestion(UploadQuestionDto uploadQuestionDto);
        Task<IEnumerable<GetQuestionDto>> ViewQuestions(int chapterId);
    }
}