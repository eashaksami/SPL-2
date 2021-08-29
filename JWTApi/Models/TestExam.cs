using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBET.Models
{
    public class TestExam
    {
        public int TestExamId { get; set; }
        public int Quantity { get; set; }
        public int TotalCorrectAnswer { get; set; }
        public int TotalWrongAnswer { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int? CourseCode { get; set; }
        [ForeignKey("CourseCode")]
        public Course Course { get; set; }
    }
}
