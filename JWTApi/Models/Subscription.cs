using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTApi.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriberId { get; set; }
        public int Price { get; set; }
        public DateTime Length { get; set; }
        public int? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public int? CourseCode { get; set; }
        [ForeignKey("CourseCode")]
        public Course Course { get; set; }
    }
}