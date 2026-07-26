namespace Application.DTOs.AuthenticationDto
{
    public class ValidLoginDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IList<string> Roles { get; set; }
    }
}