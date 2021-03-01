using System.Threading.Tasks;
using JWTApi.Data;
using JWTApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTApi.Controllers
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
        [HttpGet("{question}/{view}/{id}")]
        public async Task<IActionResult> ViewQuestion(int id)
        {
            var question = await _adminService.ViewQuestions(id);
            return Ok(question);
        }
        [AllowAnonymous]
        [HttpGet("{question}/{view}/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _adminService.ViewQuestions(id);
            return Ok(question);
        }
    }
}
