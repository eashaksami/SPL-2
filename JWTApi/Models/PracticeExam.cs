using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBET.Models
{
    public class PracticeExam
    {
        [Key]
        public int PracticeId { get; set; }
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
