using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTApi.Models
{
    public class Course
    {
        [Key]
        public int CourseCode { get; set; }
        public string Name { get; set; }
        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
        public ICollection<Chapter> Chapter { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<PracticeExam> PracticeExams { get; set; }
        public ICollection<TestExam> TestExams { get; set; }
        public ICollection<QuestionStatus> QuestionStatuses { get; set; }
    }
}
