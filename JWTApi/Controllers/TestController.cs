using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JWTApi.Data;
using JWTApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JWTApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly ITestExamRepo _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public TestController(ITestExamRepo repo, IMapper mapper, DataContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet("{chapters}/{id}")]
        public async Task<IActionResult> GetChapter(int id)
        {
            //id = 1;
            //courseName = "পদার্থবিজ্ঞান ১ম পত্র";
            var chapter = await _repo.GetChapter(id);
            var chapters = _mapper.Map<IEnumerable<ChapterDto>>(chapter);
            return Ok(chapters);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery] string studentId)
        {
            var StudentId = Convert.ToInt16(studentId);
            var course = await _repo.GetCourse(StudentId);
            // var chapters= await _context.Courses.
            // FromSqlRaw
            // ("SELECT * From Courses").ToListAsync();
            // var b = _mapper.Map<IEnumerable<CourseDto>>(chapters);
            return Ok(course);
        }

        [HttpGet("{demo}/{questions}/{exam}/{sami}")]
        public async Task<IActionResult> GetCoursesDemo()
        {
            var chapters= await _context.Courses.
            FromSqlRaw
            ("SELECT * From Courses").ToListAsync();
            var b = _mapper.Map<IEnumerable<CourseDto>>(chapters);
            return Ok(b);
        }

        [HttpPost("{exam}/{questions}")]
        public async Task<IActionResult> GetQuestions([FromBody] QuestionDto questionDto)
        {
            // var ChapterId = Convert.ToInt16(chapterId);
            // var StudentId = Convert.ToInt16(studentId);
            // var correctOrWrong = Convert.ToInt16(CorrectOrWrong);
            // var seenOrUnseen = Convert.ToInt16(SeenOrUnseen);
            if(questionDto.examType == "practiceExam"){
                var question = await _repo.GetQuestion(questionDto.studentId,questionDto.examType,questionDto.CorrectOrWrong,
                                   questionDto.SeenOrUnseen, questionDto.TotalQuestion, questionDto.chapterIds);      
                var questions = _mapper.Map<IEnumerable<GetQuestionDto>>(question);

                var rnd = new Random();
                var result = questions.OrderBy(item => rnd.Next());
                return Ok(result);
                // return Ok(result.OrderBy(x => x.QuestionId));
            }
            else{
                var question = await _repo.GetTestQuestion(questionDto.studentId,questionDto.examType,questionDto.CorrectOrWrong,
                                   questionDto.SeenOrUnseen, questionDto.TotalQuestion, questionDto.chapterIds);
                var questions = _mapper.Map<IEnumerable<TestQuestionDto>>(question);

                var rnd = new Random();
                var result = questions.OrderBy(item => rnd.Next());
                // var result = questions.OrderBy(item => rnd.Next()).Take(NoOfQuestions);
                // result.Take(HowManyQuestions);
                // var result1 = result.Take(3);
                return Ok(result.OrderBy(x => x.QuestionId));
            }
        }


        [HttpPost("{answers}")]
        public async Task<IActionResult> getAnswers([FromBody] GetAnswerDto getAnswerDto)
        {
            var question = await _context.Questions.Where(x => getAnswerDto.questionId.Contains(x.QuestionId))
            .ToListAsync();

            var answers = _mapper.Map<IEnumerable<AnswerDto>>(question);

            // var rnd = new Random();
            // var a = ((from p in _context.Questions
            //         join q in _context.QuestionStatuses on p.QuestionId equals q.QuestionId
            //         where getAnswerDto.questionId.Contains(p.QuestionId) && q.PSeenOrUnseen == 1
            //         select new{p.QuestionId})
            //         .OrderBy(item => rnd.Next()).Take(5))
            //         .Union(((from p in _context.Questions
            //         join q in _context.QuestionStatuses on p.QuestionId equals q.QuestionId
            //         where getAnswerDto.questionId.Contains(p.QuestionId) && q.PSeenOrUnseen == 0
            //         select new{p.QuestionId})
            //         .OrderBy(item => rnd.Next()).Take(5)));

            // var b = 
            // await _context.Questions.Where(x => getAnswerDto.questionId.Contains(x.QuestionId)).ToListAsync();

            return Ok(answers);
        }

        [HttpGet("{demo}/{questions}/{exam}")]
        public async Task<IActionResult> GetDemoQuestions([FromQuery] string courseCode)
        {
            var CourseCode = Convert.ToInt16(courseCode);
            var question = await _repo.GetDemoQuestion(CourseCode);
            var questions = _mapper.Map<IEnumerable<GetQuestionDto>>(question);

            // var rnd = new Random();
            // var result = questions.OrderBy(item => rnd.Next()).Take(5);

            return Ok(questions);
        }

        [HttpPost]
        public databaseUpdateDto updateDatabase([FromBody]databaseUpdateDto databaseUpdate)
        {
            int studentId = databaseUpdate.studentId;
            // int studentId = 1;
            if(databaseUpdate.examType=="practiceExam")
            {
                for(int i=0;i<databaseUpdate.questionId.Length;i++)
                {
                    // var a = _context.QuestionStatuses
                    // .FromSqlRaw("UPDATE QuestionStatuses SET PSeenOrUnseen=1, PCorrectOrWrong = {0} WHERE StudentId={1} AND QuestionId={2}",databaseUpdate.isCorrect[i],studentId,databaseUpdate.questionId[i]).ToListAsync();
                    var z = _context.QuestionStatuses.First(a => a.QuestionId == databaseUpdate.questionId[i] && a.StudentId==studentId);
                    z.PSeenOrUnseen=1;
                    z.PCorrectOrWrong=databaseUpdate.isCorrect[i];
                    _context.SaveChanges();
                }
                return databaseUpdate;
            }
            else
            {
                for(int i=0;i<databaseUpdate.questionId.Length;i++)
                {
                    var z = _context.QuestionStatuses.First(a => a.QuestionId == databaseUpdate.questionId[i] && a.StudentId==studentId);
                    z.TSeenOrUnseen=1;
                    z.TCorrectOrWrong=databaseUpdate.isCorrect[i];
                    _context.SaveChanges();
                }
                return databaseUpdate;
            }           
        }
    } 
      
}