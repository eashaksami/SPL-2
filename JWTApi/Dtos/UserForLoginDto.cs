namespace JWTApi.Dtos
{
    public class UserForLoginDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
