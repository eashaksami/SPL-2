namespace JWTApi.Dtos
{
    public class GetQuestionDto
    {
        public int QuestionId { get; set; }
        public int ChapterId { get; set; }
        public string question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string CorrectAnswer { get; set; }
        public string AnswerDetails { get; set; }
    }
}