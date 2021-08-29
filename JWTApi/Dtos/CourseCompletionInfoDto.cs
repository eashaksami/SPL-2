namespace EBET.Dtos
{
    public class CourseCompletionInfoDto
    {
        public int totalQuestion { get; set; }
        public int TotalSeen { get; set; }
        public string Name { get; set; }
    }

    public class CourseCompletionInfo
    {
        public int? ChapterId { get; set; }
        public int TotalQuestion { get; set; }
    }
}
