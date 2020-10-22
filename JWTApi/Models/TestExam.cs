using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTApi.Models
{
    public class TestExam
    {
        public int TestExamId { get; set; }
        public int Quantity { get; set; }
        public int TotalCorrectAnswer { get; set; }
        public int TotalWrongAnswer { get; set; }
        public DateTime StartTime { get; set; }
        public int? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}