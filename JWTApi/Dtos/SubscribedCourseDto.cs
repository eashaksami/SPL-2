using System;

namespace EBET.Dtos
{
    public class SubscribedCourseDto
    {
        public int CourseCode { get; set; }
        public string Name { get; set; }
        public DateTime Length { get; set; }
        public string ImageUrl { get; set; }
    }
}
