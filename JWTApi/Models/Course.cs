using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JWTApi.Models
{
    public class Course
    {
        [Key]
        public int CourseCode { get; set; }
        public string Name { get; set; }
        public ICollection<Chapter> Chapter { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}