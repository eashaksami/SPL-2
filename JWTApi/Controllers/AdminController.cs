using System;
using System.Threading.Tasks;
using EBET.Data;
using EBET.Dtos;
using EBET.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [AllowAnonymous]
        [HttpPost("{question}/{upload}")]
        public async Task<IActionResult> UploadQuestion([FromBody] UploadQuestionDto uploadQuestionDto)
        {
            var question = await _adminService.UploadQuestion(uploadQuestionDto);
            return StatusCode(201);
        }

        [AllowAnonymous]
        [HttpGet("{course}")]
        public IActionResult AddNewCourse([FromQuery] string courseName, [FromQuery] string imageId)
        {
            var ImageId = Convert.ToInt16(imageId);
            var course = _adminService.addNewCourse(courseName, ImageId);
            return Ok(course);
        }

        // [AllowAnonymous]
        [Authorize(Roles = Role.Admin)]
        [HttpGet("{question}/{view}/{id}")]
        public async Task<IActionResult> ViewQuestion(int id)
        {
            var question = await _adminService.ViewQuestions(id);
            return Ok(question);
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> UpdateQuestion(GetQuestionDto questionDto)
        {
            var question = await _adminService.UpdateQuestion(questionDto);
            return Ok(question);
        }

        [AllowAnonymous]
        [HttpDelete("{questionId:int}")]
        public async Task<IActionResult> DeleteResult([FromRoute]int questionId)
        {
            // int UserId = Convert.ToInt16(userId);
            // int CourseCode = Convert.ToInt16(courseCode);
            var result = await _adminService.DeleteQuestion(questionId);
            return Ok(201);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ViewAllStudents()
        {
            var question = await _adminService.ViewAllStudents();
            return Ok(question);
        }

        [HttpPost("{upload}")]
        public IActionResult UploadImages()
        {
            var file = Request.Form.Files[0];
            var image = _adminService.uploadImage(file);
            if (image!=null)
            {
                return Ok(image);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
