using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTApi.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string CorrectAnswer { get; set; }
        public string AnswerDetails { get; set; }
        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
        public int? ChapterId { get; set; }
        [ForeignKey("ChapterId")]
        public Chapter Chapter { get; set; }
        public ICollection<QuestionStatus> QuestionStatuses { get; set; }
    }
}