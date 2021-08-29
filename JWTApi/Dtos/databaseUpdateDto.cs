namespace EBET.Dtos
{
    public class databaseUpdateDto
    {
        public int[] questionId { get; set; }
        public int[] isCorrect { get; set; }
        public string examType { get; set; }
        public int studentId { get; set; }
    }
}