using System.Threading.Tasks;
using AutoMapper;
using EBET.Dtos;
using EBET.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Net.Http.Headers;

namespace EBET.Data
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
            var questions = (from p in _context.Questions 
                    where p.ChapterId == chapterId
                    select new
                        GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
                        Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
                        ChapterId = p.ChapterId.Value, ImageId = p.ImageId.Value}).ToList();
            int j = 0;
            foreach (var i in questions)
            {
                if(i.ImageId > 0){
                    var image = _context.Images.Where(x => x.ImageId == i.ImageId).FirstOrDefault();
                    questions[j].ImageUrl = image.ImageUrl;
                }
                j++;
            }
            return await Task.FromResult(questions);
        }

        public async Task<Question> UpdateQuestion(GetQuestionDto questionDto)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.QuestionId == questionDto.QuestionId);

            question.question = questionDto.question;
            question.Option1 = questionDto.Option1;
            question.Option2 = questionDto.Option2;
            question.Option3 = questionDto.Option3;
            question.Option4 = questionDto.Option4;
            question.AnswerDetails = questionDto.AnswerDetails;
            question.CorrectAnswer = questionDto.CorrectAnswer;
            _context.SaveChanges();
            return question;
        }

        public async Task<Question> DeleteQuestion(int questionId)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.QuestionId == questionId);
            _context.QuestionStatuses.RemoveRange
                (_context.QuestionStatuses.Where(x => x.QuestionId == question.QuestionId));
            _context.Remove(question);
            _context.SaveChanges();
            return question;
        }

        public async Task<IEnumerable<UserForLoginDto>> ViewAllStudents()
        {
            var students = from p in _context.Users 
                where p.Role == "Student"
                select new
                    UserForLoginDto{ UserId = p.UserId, Name = p.Name, Phone = p.Phone, Email = p.Email, 
                    Token = p.Token, Role = p.Role};
            return await Task.FromResult(students);
        }

        public Course addNewCourse(string courseName, int imageId)
        {
            var course = new Course
            {
                Name = courseName,
                ImageId = imageId
            };
            _context.Courses.AddAsync(course);
            _context.SaveChanges();
            return course;

        }

        public Image uploadImage(IFormFile file)
        {
            var folderName = Path.Combine("wwwroot", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = DateTime.Now.ToString("yymmssfff") +
                        ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                Image image = new Image()
                {
                    ImageUrl = fileName
                };
                _context.Images.Add(image);
                _context.SaveChanges();

                return image;
            }
            else
            {
                return null;
            }
        }

        // public async Task<Question> ViewStudentDetails()
        // {
        //     var students = from p in _context.Users 
        //                    join q in _context.Subscriptions  
        //                    on p.UserId equals q.UserId
        //                    join r in _context.Courses
        //                    on q.CourseCode equals r.CourseCode
        //                    where 
        // }
    }
}
