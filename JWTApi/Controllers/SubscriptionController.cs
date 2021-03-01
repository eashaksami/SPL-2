using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using JWTApi.Data;
using JWTApi.Dtos;
using JWTApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using JWTApi.Helpers;

namespace JWTApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SubscriptionController(ISubscriptionService subscriptionService, DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionDto subscriptionDto)
        {
            var sub = await _subscriptionService.Subscribe(subscriptionDto);
            return StatusCode(201);
        }

        [Authorize(Roles = Role.Student)]
        [HttpGet("{myCourses}")]
        public async Task<IActionResult> getSubscribedCourses([FromQuery]string studentId)
        {
            var StudentId = Convert.ToInt16(studentId);
            var courses =await _subscriptionService.GetSubscribedCourse(StudentId);
            return Ok(courses);
        }
        [HttpGet]
        public async Task<IActionResult> getImages()
        {
            var images = await _context.Images.
            FromSqlRaw
            ("SELECT * From Images").ToListAsync();
            var b = _mapper.Map<IEnumerable<ImagesDto>>(images);
            return Ok(b);
        }

        [HttpPost("{upload}")]
        public IActionResult UploadImages()
        {
            var file = Request.Form.Files[0];
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

                return Ok(new { dbPath });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

// SELECT QuestionId FROM Questions WHERE ChapterId 
// IN(SELECT ChapterId FROM Chapters WHERE CourseCode=1)

//randomize question
// var rnd = new Random();
// var result = questions.OrderBy(item => rnd.Next());
