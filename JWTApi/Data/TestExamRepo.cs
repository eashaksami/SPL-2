using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EBET.Models;
using EBET.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EBET.Data
{
    public class TestExamRepo : ITestExamRepo
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TestExamRepo(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Chapter>> GetChapter(int id)
        {
            // var course= await _context.Courses.
            // FromSqlRaw
            // ("SELECT * From Courses WHERE Courses.Name={0}",courseName).FirstOrDefaultAsync();
            // var b = _mapper.Map<CourseDto>(course);

            var values = await _context.Chapters.
            FromSqlRaw("SELECT * FROM Chapters WHERE CourseCode={0}",id).ToListAsync();
            return values;
        }

        public async Task<IEnumerable<CourseDto>> GetCourse(int studentId)
        {
            var course = await _context.CoursesModel.
            FromSqlRaw("SELECT Courses.CourseCode, Courses.Name, Images.ImageUrl FROM Courses, Images WHERE Images.ImageId == Courses.ImageId AND CourseCode NOT IN (SELECT CourseCode From Subscriptions WHERE UserId={0})",studentId).ToListAsync();
            return course;
        }

        public async Task<IEnumerable<NewGetQuestionDto>> GetDemoQuestion(int CourseCode)
        {
            var question = await _context.QuestionModel
            .FromSqlRaw("SELECT Questions.QuestionId, Questions.ChapterId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, Chapters WHERE Questions.ChapterId = Chapters.ChapterId AND Chapters.CourseCode = {0} LIMIT 10", CourseCode).ToListAsync();
            return question;
        }

        public async Task<IEnumerable<GetQuestionDto>> GetQuestion(int studentId,string examType, int correctOrWrong,
                                        int seenOrUnseen, int totalQuestion, int[] chapterIds)
        {
            // var questions = await _context.Questions.
            // FromSqlRaw("SELECT * FROM Questions WHERE Questions.ChapterId={0}", ChapterId).ToListAsync();
            //var PSeenOrUnseen=1;

            // SELECT * FROM (SELECT * FROM Questions, QuestionStatuses 
            // WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId=7
            // AND (QuestionStatuses.PSeenOrUnseen=1 AND (QuestionStatuses.PCorrectOrWrong=1 OR QuestionStatuses.PCorrectOrWrong=0))
            // LIMIT 5)
            // UNION
            // SELECT * FROM
            // (SELECT * FROM Questions, QuestionStatuses 
            // WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId=7 AND
            // QuestionStatuses.PSeenOrUnseen=0
            // LIMIT 7)

            var perChapterQuestion =(int)Math.Ceiling(((decimal)totalQuestion/chapterIds.Length));
            List<IGrouping<int, GetQuestionDto>> question = new List<IGrouping<int, GetQuestionDto>>();
            
            //correct or wrong + new
            if(correctOrWrong==0 || correctOrWrong==1)
            {
                // var question = await _context.QuestionModel
                // .FromSqlRaw("SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={0} AND (QuestionStatuses.PSeenOrUnseen={1} AND QuestionStatuses.PCorrectOrWrong={2}) LIMIT 5) UNION SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={3} AND QuestionStatuses.PSeenOrUnseen=0 LIMIT 5)",chapterId,PSeenOrUnseen,PCorrectOrWrong,ChapterId).ToListAsync();
                //         return question;
                var rnd = new Random();
                
                if(examType == "practiceExam")
                {
                    question =  (((from p in _context.Questions.AsEnumerable()
                    join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
                    where (chapterIds.Contains(p.ChapterId.Value) && ((q.PSeenOrUnseen == 1 && q.PCorrectOrWrong == correctOrWrong)
                            || q.PSeenOrUnseen == 0) && q.UserId == studentId)
                    select new
                        GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
                    Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
                    ChapterId = p.ChapterId.Value, ImageId = p.ImageId.HasValue? p.ImageId.Value: 0,
                    ImageIdForAnswer = p.ImageIdForAnswer.HasValue? p.ImageIdForAnswer.Value: 0})
                    ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
                    // .SelectMany(x => x.OrderBy(item => rnd.Next()).Take(5))
                    ).ToList();
                }
                else
                {
                    question =  (((from p in _context.Questions.AsEnumerable()
                    join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
                    where (chapterIds.Contains(p.ChapterId.Value) && ((q.TSeenOrUnseen == 1 && q.TCorrectOrWrong == correctOrWrong)
                            || q.TSeenOrUnseen == 0) && q.UserId == studentId)
                    select new
                        GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
                    Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
                    ChapterId = p.ChapterId.Value, ImageId = p.ImageId.HasValue? p.ImageId.Value: 0,
                    ImageIdForAnswer = p.ImageIdForAnswer.HasValue? p.ImageIdForAnswer.Value: 0})
                    ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
                    // .SelectMany(x => x.OrderBy(item => rnd.Next()).Take(5))
                    ).ToList();
                }

                int j = 0;
                IEnumerable<GetQuestionDto> questions = new List<GetQuestionDto>();
                foreach (var i in question)
                {
                    questions = questions.Concat(i.OrderBy(item => rnd.Next()).Take(perChapterQuestion));
                    j++;
                } 
                j = 0;
                List<GetQuestionDto> questionWithUrl = new List<GetQuestionDto>();
                questionWithUrl = questions.ToList();
                foreach (var i in questionWithUrl)
                {
                    if(i.ImageId > 0){
                        var image = _context.Images.Where(x => x.ImageId == i.ImageId).FirstOrDefault();
                        var answerImage = _context.Images.Where(x => x.ImageId == i.ImageIdForAnswer).FirstOrDefault();
                        questionWithUrl[j].ImageUrl = image.ImageUrl;
                        questionWithUrl[j].ImageUrlForAnswer = answerImage.ImageUrl;
                    }
                    j++;
                } 
                return await Task.FromResult(questionWithUrl);
            } 

            //correct + wrong + new
            else if(correctOrWrong==2)
            {
                // var ChapterId = chapterIds[1];
                //         var question = await _context.QuestionModel
                // .FromSqlRaw("SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={0} AND (QuestionStatuses.PSeenOrUnseen=1) LIMIT 5) UNION SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={1} AND QuestionStatuses.PSeenOrUnseen=0 LIMIT 7)",chapterId,ChapterId).ToListAsync();
                //         return question;
                
                var rnd = new Random();

                if(examType == "practiceExam")
                {
                    question =  (((from p in _context.Questions.AsEnumerable()
                    join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
                    where (chapterIds.Contains(p.ChapterId.Value) && (q.PSeenOrUnseen == 1 || q.PSeenOrUnseen == 0) && q.UserId == studentId)
                    select new
                        GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
                    Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
                    ChapterId = p.ChapterId.Value, ImageId = p.ImageId.HasValue? p.ImageId.Value: 0,
                    ImageIdForAnswer = p.ImageIdForAnswer.HasValue? p.ImageIdForAnswer.Value: 0})
                    ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
                    ).ToList();
                }
                
                else
                {
                    question =  (((from p in _context.Questions.AsEnumerable()
                    join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
                    where (chapterIds.Contains(p.ChapterId.Value) && (q.TSeenOrUnseen == 1 || q.TSeenOrUnseen == 0) && q.UserId == studentId)
                    select new
                        GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
                    Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
                    ChapterId = p.ChapterId.Value, ImageId = p.ImageId.HasValue? p.ImageId.Value: 0,
                    ImageIdForAnswer = p.ImageIdForAnswer.HasValue? p.ImageIdForAnswer.Value: 0})
                    ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
                    ).ToList();
                }

                int j = 0;
                IEnumerable<GetQuestionDto> questions = new List<GetQuestionDto>();
                foreach (var i in question)
                {
                    questions = questions.Concat(i.OrderBy(item => rnd.Next()).Take(perChapterQuestion));
                    j++;
                } 
                j = 0;
                List<GetQuestionDto> questionWithUrl = new List<GetQuestionDto>();
                questionWithUrl = questions.ToList();
                foreach (var i in questionWithUrl)
                {
                    if(i.ImageId > 0){
                        var image = _context.Images.Where(x => x.ImageId == i.ImageId).FirstOrDefault();
                        var answerImage = _context.Images.Where(x => x.ImageId == i.ImageIdForAnswer).FirstOrDefault();
                        questionWithUrl[j].ImageUrl = image.ImageUrl;
                        questionWithUrl[j].ImageUrlForAnswer = answerImage.ImageUrl;
                    }
                    j++;
                } 
                
                return await Task.FromResult(questions.Union(questionWithUrl));
            }  

                //only new
            else
            {
                //  var question = await _context.QuestionModel
                // .FromSqlRaw("SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={0} AND QuestionStatuses.PSeenOrUnseen=0",chapterId).ToListAsync();
                //  return question;
                
                var rnd = new Random();
                if(examType == "practiceExam")
                {
                    question =  (((from p in _context.Questions.AsEnumerable()
                    join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
                    where (chapterIds.Contains(p.ChapterId.Value) && q.PSeenOrUnseen == 0 && q.UserId == studentId)
                    select new
                        GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
                    Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
                    ChapterId = p.ChapterId.Value, ImageId = p.ImageId.HasValue? p.ImageId.Value: 0,
                    ImageIdForAnswer = p.ImageIdForAnswer.HasValue? p.ImageIdForAnswer.Value: 0})
                    ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
                    ).ToList();
                }

                else
                {
                    question =  (((from p in _context.Questions.AsEnumerable()
                    join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
                    where (chapterIds.Contains(p.ChapterId.Value) && q.TSeenOrUnseen == 0 && q.UserId == studentId)
                    select new
                        GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
                    Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
                    ChapterId = p.ChapterId.Value, ImageId = p.ImageId.HasValue? p.ImageId.Value: 0,
                    ImageIdForAnswer = p.ImageIdForAnswer.HasValue? p.ImageIdForAnswer.Value: 0})
                    ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
                    ).ToList();
                }

                int j = 0;
                IEnumerable<GetQuestionDto> unseenQuestions = new List<GetQuestionDto>();
                foreach (var i in question)
                {
                    unseenQuestions = unseenQuestions.Concat(i.OrderBy(item => rnd.Next()).Take(perChapterQuestion));
                    j++;
                }
                j = 0;
                List<GetQuestionDto> questionWithUrl = new List<GetQuestionDto>();
                questionWithUrl = unseenQuestions.ToList();
                foreach (var i in questionWithUrl)
                {
                    if(i.ImageId > 0){
                        var image = _context.Images.Where(x => x.ImageId == i.ImageId).FirstOrDefault();
                        var answerImage = _context.Images.Where(x => x.ImageId == i.ImageIdForAnswer).FirstOrDefault();
                        questionWithUrl[j].ImageUrl = image.ImageUrl;
                        questionWithUrl[j].ImageUrlForAnswer = answerImage.ImageUrl;
                    }
                    j++;
                } 
                return await Task.FromResult(questionWithUrl);
            }
        }
    }
}
