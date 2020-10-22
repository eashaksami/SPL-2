using System.Threading.Tasks;
using AutoMapper;
using JWTApi.Dtos;
using JWTApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace JWTApi.Data
{
    public class AdminService : IAdminService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AdminService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UploadQuestionDto> UploadQuestion(UploadQuestionDto uploadQuestionDto)
        {
            var question = new Question()
            {
                ChapterId = uploadQuestionDto.ChapterId,
                question = uploadQuestionDto.question,
                Option1 = uploadQuestionDto.Option1,
                Option2 = uploadQuestionDto.Option2,
                Option3 = uploadQuestionDto.Option3,
                Option4 = uploadQuestionDto.Option4,
                CorrectAnswer = uploadQuestionDto.CorrectAnswer,
                AnswerDetails = uploadQuestionDto.AnswerDetails
            };
            _context.Questions.Add(question);
            _context.SaveChanges();
            var b = _mapper.Map<UploadQuestionDto>(question);
            return await Task.FromResult(b);
        }

        public async Task<IEnumerable<GetQuestionDto>> ViewQuestions(int chapterId)
        {
            var questions = from p in _context.Questions 
                    where p.ChapterId == chapterId
                    select new
                        GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
                        Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
                        ChapterId = p.ChapterId.Value};
            return await Task.FromResult(questions);
        }
    }
}