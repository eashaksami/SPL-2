using System.Collections.Generic;

namespace EBET.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public ICollection<PracticeExam> PracticeExam { get; set; }
        public ICollection<QuestionStatus> QuestionStatuses { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<TestExam> TestExams { get; set; }
    }
}
