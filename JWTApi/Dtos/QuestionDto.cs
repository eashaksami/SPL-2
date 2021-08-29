namespace EBET.Dtos
{
    public class QuestionDto
    {
        public int studentId { get; set; }
        public string examType { get; set; }
        public int CorrectOrWrong { get; set; }
        public int SeenOrUnseen { get; set; }
        public int TotalQuestion { get; set; }
        public int[] chapterIds { get; set; }
    }
}
