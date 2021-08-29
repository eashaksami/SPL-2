using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBET.Dtos;
using EBET.Models;
using Microsoft.EntityFrameworkCore;

namespace EBET.Data
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly DataContext _context;

        public SubscriptionService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubscribedCourseDto>> GetSubscribedCourse(int studentId)
        {
            var update = _context.Subscriptions.Where(w => w.Length <= DateTime.Now).ToList();
            foreach(var i in update)
            {
                removeStatus(i.UserId.Value, i.CourseCode.Value);
                _context.Remove(i);
            }
            _context.SaveChanges();
            var courses = await _context.CourseModel.
            FromSqlRaw("SELECT Courses.Name, Subscriptions.Length, Courses.CourseCode, Images.ImageUrl FROM Courses, Subscriptions, Images WHERE Courses.CourseCode=Subscriptions.CourseCode AND Courses.ImageId=Images.ImageId AND Subscriptions.UserId={0}",studentId).ToListAsync(); 
            _context.SaveChanges();
            return courses;
        }

        void removeStatus(int studentId, int CourseCode)
        {
            var question = _context.QuestionModel.FromSqlRaw("SELECT ChapterId, QuestionId, question, Option1, Option2, Option3, Option4, CorrectAnswer, AnswerDetails FROM Questions WHERE ChapterId IN(SELECT ChapterId FROM Chapters WHERE CourseCode={0})", CourseCode).ToList();        
            foreach (var i in question)
            {
                // db.People.RemoveRange(db.People.Where(x => x.State == "CA"));
                _context.QuestionStatuses.RemoveRange
                (_context.QuestionStatuses.Where(x => x.UserId == studentId && x.QuestionId == i.QuestionId));
            }
        }

        public async Task<Subscription> Subscribe(SubscriptionDto subscriptionDto)
        {
            var sub = new Subscription()
            {
                Price = subscriptionDto.Price,
                Length = DateTime.Now.AddMonths(subscriptionDto.Length),
                // Length = DateTime.Now.AddMinutes(2),
                // Length = DateTime.UtcNow.Date,
                UserId = subscriptionDto.StudentId,
                CourseCode = subscriptionDto.CourseCode
            };
            _context.Subscriptions.Add(sub);
            _context.SaveChanges();
            updateDatabase(subscriptionDto.StudentId, subscriptionDto.CourseCode);
            return await Task.FromResult(sub);
        }

        public List<NewGetQuestionDto> question { get; set; }

        void updateDatabase(int studentId, int CourseCode)
        {
            question = _context.QuestionModel.FromSqlRaw("SELECT ChapterId, QuestionId, question, Option1, Option2, Option3, Option4, CorrectAnswer, AnswerDetails FROM Questions WHERE ChapterId IN(SELECT ChapterId FROM Chapters WHERE CourseCode={0})", CourseCode).ToList();        
            
            question = question.ToList();

            foreach (var i in question)
            {
                    var z = new QuestionStatus()
                    {
                        UserId = studentId,
                        QuestionId = i.QuestionId,
                        CourseCode = CourseCode,
                        PSeenOrUnseen=0,
                        TSeenOrUnseen = 0,
                        PCorrectOrWrong = 0,
                        TCorrectOrWrong = 0,
                    };
                    _context.QuestionStatuses.Add(z);
                    _context.SaveChanges();
            }
        }
    }
}
