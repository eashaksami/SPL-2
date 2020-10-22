namespace JWTApi.Dtos
{
    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public string CorrectAnswer { get; set; }
        public string AnswerDetails { get; set; }
    }

    public class GetAnswerDto
    {
        public int[] questionId { get; set; }
    }
}