using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTApi.Models
{
    public class PracticeExam
    {
        [Key]
        public int PracticeId { get; set; }
        public int Quantity { get; set; }
        public int TotalCorrectAnswer { get; set; }
        public int TotalWrongAnswer { get; set; }
        public int? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}