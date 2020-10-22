using System.ComponentModel.DataAnnotations.Schema;

namespace JWTApi.Models
{
    public class QuestionStatus
    {
        public int PSeenOrUnseen { get; set; }
        public int TSeenOrUnseen { get; set; }
        public int PCorrectOrWrong { get; set; }
        public int TCorrectOrWrong { get; set; }
        // [Key]
        // [Column(Order = 1)]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        // [Key]
        // [Column(Order = 2)]
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
    }
}