// using System.Collections.Generic;
// using System.Threading.Tasks;
// using AutoMapper;
// using JWTApi.Models;
// using JWTApi.Dtos;
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Linq;

// namespace JWTApi.Data
// {
//     public class TestExamRepo : ITestExamRepo
//     {
//         private readonly DataContext _context;
//         private readonly IMapper _mapper;

//         public TestExamRepo(DataContext context,IMapper mapper)
//         {
//             _context = context;
//             _mapper = mapper;
//         }

//         public async Task<IEnumerable<Chapter>> GetChapter(int id)
//         {
//             // var course= await _context.Courses.
//             // FromSqlRaw
//             // ("SELECT * From Courses WHERE Courses.Name={0}",courseName).FirstOrDefaultAsync();
//             // var b = _mapper.Map<CourseDto>(course);

//             var values = await _context.Chapters.
//             FromSqlRaw("SELECT * FROM Chapters WHERE CourseCode={0}",id).ToListAsync();
//             return values;
//         }

//         public async Task<IEnumerable<CourseDto>> GetCourse(int studentId)
//         {
//             var course = await _context.Course.
//             FromSqlRaw("SELECT CourseCode, Name FROM Courses WHERE CourseCode NOT IN (SELECT CourseCode From Subscriptions WHERE StudentId={0})",studentId).ToListAsync();
//             return course;
//         }

//         public async Task<IEnumerable<GetQuestionDto>> GetDemoQuestion(int CourseCode)
//         {
//             var question = await _context.QuestionModel
//             .FromSqlRaw("SELECT Questions.QuestionId, Questions.ChapterId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, Chapters WHERE Questions.ChapterId = Chapters.ChapterId AND Chapters.CourseCode = {0} LIMIT 10", CourseCode).ToListAsync();
//             return question;
//         }

//         public async Task<IEnumerable<GetQuestionDto>> GetQuestion(int studentId,string examType, int correctOrWrong,
//                                         int seenOrUnseen, int totalQuestion, int[] chapterIds)
//         {

//             // var questions = await _context.Questions.
//             // FromSqlRaw("SELECT * FROM Questions WHERE Questions.ChapterId={0}", ChapterId).ToListAsync();
//             //var PSeenOrUnseen=1;

//             // SELECT * FROM (SELECT * FROM Questions, QuestionStatuses 
//             // WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId=7
//             // AND (QuestionStatuses.PSeenOrUnseen=1 AND (QuestionStatuses.PCorrectOrWrong=1 OR QuestionStatuses.PCorrectOrWrong=0))
//             // LIMIT 5)
//             // UNION
//             // SELECT * FROM
//             // (SELECT * FROM Questions, QuestionStatuses 
//             // WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId=7 AND
//             // QuestionStatuses.PSeenOrUnseen=0
//             // LIMIT 7)
            
//             int[] seen = new int[chapterIds.Length];
//             int[] unseen = new int[chapterIds.Length];
//             int[] correct = new int[chapterIds.Length];
//             int[] wrong = new int[chapterIds.Length];
//             int[] returned = new int[chapterIds.Length*2];
//             int totalSeen = (int)Math.Ceiling(totalQuestion*(.3));
//             int totalUnseen = totalQuestion - totalSeen;
//             int perChapterSeen = (int)Math.Ceiling(((decimal)totalSeen/chapterIds.Length));
//             int perChapterUnseen = (int)Math.Ceiling(((decimal)totalUnseen/chapterIds.Length));

//             if(correctOrWrong!=3){
//                 returned = othetCalculations(chapterIds, totalSeen, totalUnseen, perChapterSeen, perChapterUnseen, studentId,
//                                                 correctOrWrong, totalQuestion, examType);
                
//                 seen = returned.Take(returned.Length/2).ToArray();
//                 unseen = returned.Skip(returned.Length/2).ToArray();
//             }

//             //correct or wrong + new
//             if(correctOrWrong==0 || correctOrWrong==1)
//                 {
//             // var question = await _context.QuestionModel
//             // .FromSqlRaw("SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={0} AND (QuestionStatuses.PSeenOrUnseen={1} AND QuestionStatuses.PCorrectOrWrong={2}) LIMIT 5) UNION SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={3} AND QuestionStatuses.PSeenOrUnseen=0 LIMIT 5)",chapterId,PSeenOrUnseen,PCorrectOrWrong,ChapterId).ToListAsync();
//             //         return question;
//                     var rnd = new Random();
//                     var question =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.PSeenOrUnseen == seenOrUnseen && q.PCorrectOrWrong == correctOrWrong
//                             && q.StudentId == studentId)
//                     select new
//                         GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
//                     ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     // .SelectMany(x => x.OrderBy(item => rnd.Next()).Take(5))
//                     ).ToList();

//                     var unseenQuestion =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.PSeenOrUnseen == 0 && q.StudentId == studentId)
//                     select new
//                         GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
//                     ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     ).ToList();
//                     int j = 0;
//                     IEnumerable<GetQuestionDto> questions = new List<GetQuestionDto>();
//                     IEnumerable<GetQuestionDto> unseenQuestions = new List<GetQuestionDto>();
//                     foreach (var i in question)
//                     {
//                         questions = questions.Concat(i.OrderBy(item => rnd.Next()).Take(seen[j]));
//                         j++;
//                     } 
//                     j=0;
//                     foreach (var i in unseenQuestion)
//                     {
//                         unseenQuestions = unseenQuestions.Concat(i.OrderBy(item => rnd.Next()).Take(unseen[j]));
//                         j++;
//                     }
//                     return await Task.FromResult(questions.Union(unseenQuestions));
//                 } 
//                 //correct + wrong + new
//                 else if(correctOrWrong==2)
//                 {
//                     // var ChapterId = chapterIds[1];
//             //         var question = await _context.QuestionModel
//             // .FromSqlRaw("SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={0} AND (QuestionStatuses.PSeenOrUnseen=1) LIMIT 5) UNION SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={1} AND QuestionStatuses.PSeenOrUnseen=0 LIMIT 7)",chapterId,ChapterId).ToListAsync();
//             //         return question;
                    
//                     int extraQuestion = ((perChapterSeen + perChapterUnseen)*chapterIds.Length) - totalQuestion;
//                     int x = 0;
//                     while (true)
//                     {
//                         if(extraQuestion==0)break;
//                         if(seen[x] > unseen[x]) seen[x] = seen[x] - 1;
//                         else unseen[x] = unseen[x] - 1;
//                         extraQuestion--;
//                         x++;
//                     }
//                     var rnd = new Random();
//                     var question =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.PSeenOrUnseen == 1 && q.StudentId == studentId)
//                     select new
//                         GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
//                     ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     ).ToList();

//                     var unseenQuestion =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.PSeenOrUnseen == 0 && q.StudentId == studentId)
//                     select new
//                         GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
//                     ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     ).ToList();
//                     int j = 0;
//                     IEnumerable<GetQuestionDto> questions = new List<GetQuestionDto>();
//                     IEnumerable<GetQuestionDto> unseenQuestions = new List<GetQuestionDto>();
//                     foreach (var i in question)
//                     {
//                         questions = questions.Concat(i.OrderBy(item => rnd.Next()).Take(seen[j]));
//                         j++;
//                     } 
//                     j=0;
//                     foreach (var i in unseenQuestion)
//                     {
//                         unseenQuestions = unseenQuestions.Concat(i.OrderBy(item => rnd.Next()).Take(unseen[j]));
//                         j++;
//                     }
//                     return await Task.FromResult(questions.Union(unseenQuestions));
//                 }  
//                 //only new
//                 else
//                 {
//         //         var question = await _context.QuestionModel
//             // .FromSqlRaw("SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer, Questions.AnswerDetails FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={0} AND QuestionStatuses.PSeenOrUnseen=0",chapterId).ToListAsync();
//             //         return question;

//                     unseen = getUnseenArray(totalQuestion, chapterIds, studentId, examType);
                    
//                     var rnd = new Random();
//                     var unseenQuestion =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.PSeenOrUnseen == 0 && q.StudentId == studentId)
//                     select new
//                         GetQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, AnswerDetails = p.AnswerDetails,
//                     ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     ).ToList();

//                     int j = 0;
//                     IEnumerable<GetQuestionDto> unseenQuestions = new List<GetQuestionDto>();
//                     foreach (var i in unseenQuestion)
//                     {
//                         unseenQuestions = unseenQuestions.Concat(i.OrderBy(item => rnd.Next()).Take(unseen[j]));
//                         j++;
//                     }
//                     return await Task.FromResult(unseenQuestions);
//                 }
//         }

//         public async Task<IEnumerable<TestQuestionDto>> GetTestQuestion(int studentId,string examType, int correctOrWrong,
//                                         int seenOrUnseen, int totalQuestion, int[] chapterIds)
//         {
//             int[] seen = new int[chapterIds.Length];
//             int[] unseen = new int[chapterIds.Length];
//             int[] correct = new int[chapterIds.Length];
//             int[] wrong = new int[chapterIds.Length];
//             int[] returned = new int[chapterIds.Length*2];
//             int totalSeen = (int)Math.Ceiling(totalQuestion*(.3));
//             int totalUnseen = totalQuestion - totalSeen;
//             int perChapterSeen = (int)Math.Ceiling(((decimal)totalSeen/chapterIds.Length));
//             int perChapterUnseen = (int)Math.Ceiling(((decimal)totalUnseen/chapterIds.Length));

//             if(correctOrWrong!=3){
//                 returned = othetCalculations(chapterIds, totalSeen, totalUnseen, perChapterSeen, perChapterUnseen, studentId,
//                                                 correctOrWrong, totalQuestion, examType);
                
//                 seen = returned.Take(returned.Length/2).ToArray();
//                 unseen = returned.Skip(returned.Length/2).ToArray();
//             }

//             //correct or wrong + new
//             if(correctOrWrong==0 || correctOrWrong==1)
//                 {
//             //         var ChapterId = chapterId;
//             // //         var question = await _context.TestQuestionModel
//             // // .FromSqlRaw("SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={0} AND (QuestionStatuses.TSeenOrUnseen={1} AND QuestionStatuses.TCorrectOrWrong={2}) LIMIT 5) UNION SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={3} AND QuestionStatuses.TSeenOrUnseen=0 LIMIT 5)",chapterId,TSeenOrUnseen,TCorrectOrWrong,ChapterId).ToListAsync();
                    
//                     var rnd = new Random();
//                     var question =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.TSeenOrUnseen == seenOrUnseen && q.TCorrectOrWrong == correctOrWrong
//                             && q.StudentId == studentId)
//                     select new
//                         TestQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     // .SelectMany(x => x.OrderBy(item => rnd.Next()).Take(5))
//                     ).ToList();

//                     var unseenQuestion =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.TSeenOrUnseen == 0 && q.StudentId == studentId)
//                     select new
//                         TestQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     ).ToList();
//                     int j = 0;
//                     IEnumerable<TestQuestionDto> questions = new List<TestQuestionDto>();
//                     IEnumerable<TestQuestionDto> unseenQuestions = new List<TestQuestionDto>();
//                     foreach (var i in question)
//                     {
//                         questions = questions.Concat(i.OrderBy(item => rnd.Next()).Take(seen[j]));
//                         j++;
//                     } 
//                     j=0;
//                     foreach (var i in unseenQuestion)
//                     {
//                         unseenQuestions = unseenQuestions.Concat(i.OrderBy(item => rnd.Next()).Take(unseen[j]));
//                         j++;
//                     }
//                     return await Task.FromResult(questions.Union(unseenQuestions));
//                 } 
//                 //correct + wrong + new
//                 else if(correctOrWrong==2)
//                 {
//                     // var ChapterId = chapterIds[1];
//             //         var question = await _context.TestQuestionModel
//             // .FromSqlRaw("SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={0} AND (QuestionStatuses.TSeenOrUnseen=1) LIMIT 5) UNION SELECT * FROM (SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={1} AND QuestionStatuses.TSeenOrUnseen=0 LIMIT 7)",chapterId,ChapterId).ToListAsync();
//             //         return question;
                    
//                     int extraQuestion = ((perChapterSeen + perChapterUnseen)*chapterIds.Length) - totalQuestion;
//                     int x = 0;
//                     while (true)
//                     {
//                         if(extraQuestion==0)break;
//                         if(seen[x] > unseen[x]) seen[x] = seen[x] - 1;
//                         else unseen[x] = unseen[x] - 1;
//                         extraQuestion--;
//                         x++;
//                     }
//                     var rnd = new Random();
//                     var question =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.TSeenOrUnseen == 1 && q.StudentId == studentId)
//                     select new
//                         TestQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     ).ToList();

//                     var unseenQuestion =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.TSeenOrUnseen == 0 && q.StudentId == studentId)
//                     select new
//                         TestQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     ).ToList();
//                     int j = 0;
//                     IEnumerable<TestQuestionDto> questions = new List<TestQuestionDto>();
//                     IEnumerable<TestQuestionDto> unseenQuestions = new List<TestQuestionDto>();
//                     foreach (var i in question)
//                     {
//                         questions = questions.Concat(i.OrderBy(item => rnd.Next()).Take(seen[j]));
//                         j++;
//                     } 
//                     j=0;
//                     foreach (var i in unseenQuestion)
//                     {
//                         unseenQuestions = unseenQuestions.Concat(i.OrderBy(item => rnd.Next()).Take(unseen[j]));
//                         j++;
//                     }
//                     return await Task.FromResult(questions.Union(unseenQuestions));
//                 }  
//                 //only new
//                 else
//                 {
//             //         var question = await _context.TestQuestionModel
//             // .FromSqlRaw("SELECT Questions.QuestionId, Questions.question, Questions.Option1, Questions.Option2, Questions.Option3, Questions.Option4, Questions.CorrectAnswer FROM Questions, QuestionStatuses WHERE Questions.QuestionId = QuestionStatuses.QuestionId AND Questions.ChapterId={0} AND QuestionStatuses.TSeenOrUnseen=0",chapterId).ToListAsync();
//             //         return question;
//                     unseen = getUnseenArray(totalQuestion, chapterIds, studentId, examType);
                    
//                     var rnd = new Random();
//                     var unseenQuestion =  (((from p in _context.Questions.AsEnumerable()
//                     join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                     where (chapterIds.Contains(p.ChapterId.Value) && q.TSeenOrUnseen == 0 && q.StudentId == studentId)
//                     select new
//                         TestQuestionDto{ QuestionId = p.QuestionId, question = p.question, Option1 = p.Option1, Option2 = p.Option2, 
//                     Option3 = p.Option3, Option4 = p.Option4, CorrectAnswer = p.CorrectAnswer, ChapterId = p.ChapterId.Value})
//                     ).OrderBy(x => x.ChapterId).GroupBy(x => x.ChapterId)
//                     ).ToList();

//                     int j = 0;
//                     IEnumerable<TestQuestionDto> unseenQuestions = new List<TestQuestionDto>();
//                     foreach (var i in unseenQuestion)
//                     {
//                         unseenQuestions = unseenQuestions.Concat(i.OrderBy(item => rnd.Next()).Take(unseen[j]));
//                         j++;
//                     }
//                     return await Task.FromResult(unseenQuestions);
//                 }
//         }

//         int[] getUnseenArray(int totalQuestion, int[] chapterIds, int studentId, string examType)
//         {
//             int[] unseen = new int[chapterIds.Length];
//             int[] take = new int[chapterIds.Length];
//             int[] temp = new int[chapterIds.Length];
//             int[] tempU = new int[chapterIds.Length];
//             int count = 0;
//             int perChapterTake = (int)Math.Ceiling((double)totalQuestion/chapterIds.Length);
//             int extraQuestion = perChapterTake*chapterIds.Length - totalQuestion;
//             for(int i = 0; i < chapterIds.Length; i++)
//             {
//                 if(extraQuestion !=0)
//                 {
//                     take[i] = perChapterTake - 1;
//                     extraQuestion--;
//                 }
//                 else take[i] = perChapterTake;                     
//             }
//             var a = from p in _context.Questions.AsEnumerable()
//             join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//             select new{ QuestionId = p.QuestionId, StudentId = q.StudentId, ChapterId = p.ChapterId, TSeenOrUnseen = q.TSeenOrUnseen,
//                         PSeenOrUnseen = q.PSeenOrUnseen};

//             for(var i = 0; i < chapterIds.Length; i++)
//             {
//                 if(examType == "testExam")
//                 {
//                     unseen[i] = a.Where(p => p.StudentId == studentId && p.ChapterId == chapterIds[i] && p.TSeenOrUnseen == 0)
//                             .Select(x => new{ x.QuestionId}).Count();
//                 }
//                 else
//                 {
//                     unseen[i] = a.Where(p => p.StudentId == studentId && p.ChapterId == chapterIds[i] && p.PSeenOrUnseen == 0)
//                             .Select(x => new{ x.QuestionId}).Count();
//                 }
//                 if(unseen[i] > take[i])
//                 {
//                     temp[i] = unseen[i] - take[i];
//                     tempU[i] = take[i];
//                     count++;
//                 }
//                 else
//                 {
//                     temp[i] = 1000;
//                     tempU[i] = unseen[i];
//                 } 
//             }
//             int remainQuestions = totalQuestion - tempU.Sum();
//             int addPerChapter = 0;
//             if(count > 0)
//             {
//                 addPerChapter = (int)Math.Ceiling((double)remainQuestions/count);
//             } 
//             int k = 0;
//             while (remainQuestions!=0)
//             {
//                 if(k == count) break;
//                 if(remainQuestions < addPerChapter) addPerChapter = remainQuestions;
//                 int minIndex = Array.IndexOf(temp, temp.Min());
//                 if(temp[minIndex] >= addPerChapter)
//                 {
//                     tempU[minIndex] = tempU[minIndex] + addPerChapter;
//                     remainQuestions = remainQuestions - addPerChapter;
//                     temp[minIndex] = 1000;
//                 }
//                 else
//                 {
//                     tempU[minIndex] = tempU[minIndex] + temp[minIndex];
//                     remainQuestions = remainQuestions - temp[minIndex];
//                     temp[minIndex] = 1000;
//                 }
//                 k++;
//             }
//             return tempU;
//         }

//         int[] othetCalculations(int[] chapterIds, int totalSeen, int totalUnseen, int perChapterSeen,
//                                         int perChapterUnseen, int studentId, int correctOrWrong, int totalQuestion, string examType)
//         {
//             int[] seen = new int[chapterIds.Length];
//             int[] unseen = new int[chapterIds.Length];
//             int[] correct = new int[chapterIds.Length];
//             int[] wrong = new int[chapterIds.Length];

//             var a = from p in _context.Questions.AsEnumerable()
//                 join q in _context.QuestionStatuses.AsEnumerable() on p.QuestionId equals q.QuestionId
//                 select new{ QuestionId = p.QuestionId, StudentId = q.StudentId, TSeenOrUnseen = q.TSeenOrUnseen, 
//                 TCorrectOrWrong = q.TCorrectOrWrong, ChapterId = p.ChapterId, PCorrectOrWrong = q.PCorrectOrWrong, PSeenOrUnseen = q.PSeenOrUnseen};

//             if(examType=="practiceExam")
//             {
//                 for(var i = 0; i < chapterIds.Length; i++)
//                 {
//                     unseen[i] = a.Where(p => p.StudentId == studentId && p.PSeenOrUnseen==0 && p.ChapterId == chapterIds[i])
//                                 .Select(x => new{ x.QuestionId}).Count();
//                     seen[i] = a.Where(p => p.StudentId == studentId && p.PSeenOrUnseen==1 && p.ChapterId == chapterIds[i])
//                                 .Select(x => new{ x.QuestionId}).Count();
                    
//                     correct[i] = a.Where(p => p.StudentId == studentId && p.PSeenOrUnseen==1 && p.ChapterId == chapterIds[i]
//                                         && p.PCorrectOrWrong == 1).Select(x => new{ x.QuestionId}).Count();
//                     wrong[i] = seen[i] - correct[i];
//                 }
//             }

//             else
//             {
//                 for(var i = 0; i < chapterIds.Length; i++)
//                 {
//                     unseen[i] = a.Where(p => p.StudentId == studentId && p.TSeenOrUnseen==0 && p.ChapterId == chapterIds[i])
//                                 .Select(x => new{ x.QuestionId}).Count();
//                     seen[i] = a.Where(p => p.StudentId == studentId && p.TSeenOrUnseen==1 && p.ChapterId == chapterIds[i])
//                                 .Select(x => new{ x.QuestionId}).Count();
                    
//                     correct[i] = a.Where(p => p.StudentId == studentId && p.TSeenOrUnseen==1 && p.ChapterId == chapterIds[i]
//                                         && p.TCorrectOrWrong == 1).Select(x => new{ x.QuestionId}).Count();
//                     wrong[i] = seen[i] - correct[i];
//                 }
//             }
            
//             // return wrong.Concat(unseen).ToArray();

//             if(correctOrWrong == 0)
//             {
//                 int isChecked = 0;
//                 int[] tempW = new int[chapterIds.Length];
//                 int[] tempU = new int[chapterIds.Length];
//                 int[] arr = new int[chapterIds.Length*2];
//                 // for(var i = 0; i < arr.Length; i++)arr[i] = 1000;
//                 int perChapterQuestion = perChapterSeen + perChapterUnseen;

//                 for(var i = 0; i < chapterIds.Length; i++)
//                 {
//                     if(wrong[i] + unseen[i] <= perChapterQuestion)
//                     {
//                         tempW[i] = wrong[i];
//                         tempU[i] = unseen[i];
//                         arr[i] = arr[i+chapterIds.Length] = 1000;
//                     }
//                     else
//                     {
//                         if(wrong[i] < perChapterSeen)
//                         {
//                             tempW[i] = wrong[i];
//                             tempU[i] = perChapterSeen + perChapterUnseen - wrong[i];
//                             arr[i] = 1000;
//                             arr[i + chapterIds.Length] = unseen[i] - tempU[i];
//                             isChecked = 1;
//                         }
//                         else
//                         {
//                             tempW[i] = perChapterSeen;
//                             arr[i] = wrong[i] - tempW[i];
//                         }
                            

//                         if(unseen[i] < perChapterUnseen)
//                         {
//                             tempU[i] = unseen[i];
//                             tempW[i] = perChapterSeen + perChapterUnseen - unseen[i];
//                             arr[i] = wrong[i] - tempW[i];
//                             arr[i + chapterIds.Length] = 1000;
//                         }
//                         else if(unseen[i] > perChapterUnseen && isChecked == 0)
//                         { 
//                             tempU[i] = perChapterUnseen;
//                             arr[i + chapterIds.Length] = unseen[i] - tempU[i];
//                         } 
//                         isChecked = 0;
//                     }
//                 }

//                 int length = 0;
//                 int add = 0;
//                 for(int i = 0; i < arr.Length; i++)
//                 {
//                     if(arr[i]<1000)length++;
//                 }
//                 add = ((tempW.Sum() + tempU.Sum()) - totalQuestion);
//                 int addPerChapter=0;
//                 if(add>0 && length > 0)
//                 addPerChapter = (int)Math.Ceiling(((double)add/length));
//                 if(add<0 && length > 0)
//                 addPerChapter = (int)Math.Floor(((double)add/length));
//                 int minIndex = 0;
                
//                 int j =0;
//                 while (add!=0)
//                 {
//                     if(j == length)break;
//                     if(add<addPerChapter)addPerChapter=add;
//                     j++;
//                     minIndex = Array.IndexOf(arr, arr.Min());

//                         if(minIndex >= chapterIds.Length)
//                         {
//                             if(arr[minIndex] > addPerChapter) 
//                             {
//                                 tempU[minIndex - chapterIds.Length] = tempU[minIndex - chapterIds.Length] - addPerChapter;
//                                 arr[minIndex] = 1000;
//                                 add = add - addPerChapter;
//                             }
//                             else
//                             {
//                                 tempU[minIndex - chapterIds.Length] = tempU[minIndex - chapterIds.Length] - arr[minIndex];
//                                 add = add - arr[minIndex];
//                                 arr[minIndex] = 1000;
//                             }
//                         }
//                         else
//                         {
//                             if(arr[minIndex] > addPerChapter)
//                             { 
//                                 tempW[minIndex] = tempW[minIndex] - addPerChapter;
//                                 arr[minIndex] = 1000;
//                                 add = add - addPerChapter;
//                             }
//                             else
//                             {
//                                 tempW[minIndex] = tempW[minIndex] - arr[minIndex];
//                                 add = add - arr[minIndex];
//                                 arr[minIndex] = 1000;
//                             }
//                         }
//                 }
//                 // minIndex = Array.IndexOf(arr, arr.Min());
//                 // for(var i=0;i<chapterIds.Length;i++){tempW[i]=minIndex;tempU[i]=minIndex;}
                
//                 return tempW.Concat(tempU).ToArray();
//                 // return arr;
//             }

//             else if(correctOrWrong == 1)
//             {
//                 int isChecked = 0;
//                 int[] tempC = new int[chapterIds.Length];
//                 int[] tempU = new int[chapterIds.Length];
//                 int[] arr = new int[chapterIds.Length*2];
//                 int perChapterQuestion = perChapterSeen + perChapterUnseen;

//                 for(var i = 0; i < chapterIds.Length; i++)
//                 {
//                     if(correct[i] + unseen[i] <= perChapterQuestion)
//                     {
//                         tempC[i] = correct[i];
//                         tempU[i] = unseen[i];
//                         arr[i] = arr[i+chapterIds.Length] = 1000;
//                     }
//                     else
//                     {
//                         if(correct[i] < perChapterSeen)
//                         {
//                             tempC[i] = correct[i];
//                             tempU[i] = perChapterSeen + perChapterUnseen - correct[i];
//                             arr[i] = 1000;
//                             arr[i + chapterIds.Length] = unseen[i] - tempU[i];
//                             isChecked = 1;
//                         }
//                         else
//                         {
//                             tempC[i] = perChapterSeen;
//                             arr[i] = correct[i] - tempC[i];
//                         }
                            

//                         if(unseen[i] < perChapterUnseen)
//                         {
//                             tempU[i] = unseen[i];
//                             tempC[i] = perChapterSeen + perChapterUnseen - unseen[i];
//                             arr[i] = correct[i] - tempC[i];
//                             arr[i + chapterIds.Length] = 1000;
//                         }
//                         else if(unseen[i] > perChapterUnseen && isChecked == 0)
//                         { 
//                             tempU[i] = perChapterUnseen;
//                             arr[i + chapterIds.Length] = unseen[i] - tempU[i];
//                         } 
//                         isChecked = 0;
//                     }
//                 }

//                 int length = 0;
//                 int add = 0;
//                 for(int i = 0; i < arr.Length; i++)
//                 {
//                     if(arr[i]<1000)length++;
//                 }
//                 add = ((tempC.Sum() + tempU.Sum()) - totalQuestion);
//                 int addPerChapter=0;
//                 if(add>0 && length > 0)
//                 addPerChapter = (int)Math.Ceiling(((double)add/length));
//                 if(add<0 && length > 0)
//                 addPerChapter = (int)Math.Floor(((double)add/length));
//                 int minIndex = 0;
                
//                 int j =0;
//                 while (add!=0)
//                 {
//                     if(j == length)break;
//                     if(add<addPerChapter)addPerChapter=add;
//                     j++;
//                     minIndex = Array.IndexOf(arr, arr.Min());

//                         if(minIndex >= chapterIds.Length)
//                         {
//                             if(arr[minIndex] > addPerChapter) 
//                             {
//                                 tempU[minIndex - chapterIds.Length] = tempU[minIndex - chapterIds.Length] - addPerChapter;
//                                 arr[minIndex] = 1000;
//                                 add = add - addPerChapter;
//                             }
//                             else
//                             {
//                                 tempU[minIndex - chapterIds.Length] = tempU[minIndex - chapterIds.Length] - arr[minIndex];
//                                 add = add - arr[minIndex];
//                                 arr[minIndex] = 1000;
//                             }
//                         }
//                         else
//                         {
//                             if(arr[minIndex] > addPerChapter)
//                             { 
//                                 tempC[minIndex] = tempC[minIndex] - addPerChapter;
//                                 arr[minIndex] = 1000;
//                                 add = add - addPerChapter;
//                             }
//                             else
//                             {
//                                 tempC[minIndex] = tempC[minIndex] - arr[minIndex];
//                                 add = add - arr[minIndex];
//                                 arr[minIndex] = 1000;
//                             }
//                         }
//                 }
                
//                 return tempC.Concat(tempU).ToArray();
//             }

//             else
//             {
//                 int isChecked = 0;
//                 for(var i = 0; i < chapterIds.Length; i++)
//                 {
//                     if(seen[i] < perChapterSeen){ unseen[i] = perChapterUnseen + perChapterSeen - seen[i]; isChecked = 1;}
//                     else seen[i] = perChapterSeen;

//                     if(unseen[i] < perChapterUnseen) seen[i] = perChapterSeen + perChapterUnseen - unseen[i];
//                     else if(unseen[i] > perChapterUnseen && isChecked == 0) unseen[i] = perChapterUnseen;

//                     isChecked = 0;
//                 }
                
//                 return seen.Concat(unseen).ToArray();
//             }
//         }
//     }
// }