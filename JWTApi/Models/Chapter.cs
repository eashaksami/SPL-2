using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTApi.Models
{
    public class Chapter
    {
        public int ChapterId { get; set; }
        public string Name { get; set; }
        public int? CourseCode { get; set; }
        [ForeignKey("CourseCode")]
        public Course Course { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}