namespace AuthenticationModule.Models
{
    public class UserResponse
    {
        public int Id { get; set; } = 0;
        public string Token { get; set; } = null;
        public string Message { get; set; }
    }
}
