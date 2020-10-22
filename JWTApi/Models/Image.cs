using System.Collections.Generic;

namespace JWTApi.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}