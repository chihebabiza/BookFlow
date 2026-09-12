namespace BookFlow.Core.DTOs.User;
    public class UserLoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }

