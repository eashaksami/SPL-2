using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JWTApi.Data;
using JWTApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JWTApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : Controller
    {
        private readonly IPerformanceService _performanceService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PerformanceController(IPerformanceService performanceService, DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _performanceService = performanceService;
        }

        [HttpGet]
        public async Task<IActionResult> getProgressData([FromQuery]string studentId, [FromQuery] string courseCode)
        {
            var StudentId = Convert.ToInt16(studentId);
            var CourseCode = Convert.ToInt16(courseCode);
            var data = await _performanceService.GetProgressInfo(StudentId, CourseCode);
            var newData = _mapper.Map<IEnumerable<ProgressInfoDto>>(data);
            return Ok(newData);
        }

        [HttpGet("{courseCompletion}")]
        public async Task<IActionResult> getCourseCompletionData([FromQuery]string studentId, [FromQuery] string courseCode)
        {
            var StudentId = Convert.ToInt16(studentId);
            var CourseCode = Convert.ToInt16(courseCode);
            var data = await _performanceService.GetCourseCompletionInfo(StudentId, CourseCode);
            // var newData = _mapper.Map<IEnumerable<ProgressInfoDto>>(data);
            return Ok(data);
        }
    }
}