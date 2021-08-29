using System.ComponentModel.DataAnnotations.Schema;

namespace EBET.Models
{
    public class QuestionStatus
    {
        public int PSeenOrUnseen { get; set; }
        public int TSeenOrUnseen { get; set; }
        public int PCorrectOrWrong { get; set; }
        public int TCorrectOrWrong { get; set; }
        // [Key]
        // [Column(Order = 1)]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        // [Key]
        // [Column(Order = 2)]
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public int CourseCode { get; set; }
        [ForeignKey("CourseCode")]
        public Course Course { get; set; }
    }
}
