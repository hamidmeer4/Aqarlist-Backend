namespace Aqarlist.Core.Models.Dto
{
    public class UserDto
    {
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public long NationalId { get; set; }
    }
    public class LoginDto
    {
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class AuthTokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Issued { get; set; }
        public DateTime? Expiry { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
    public class LoginRepsonse
    {
        public AuthTokenResponse Token { get; set; } = new AuthTokenResponse();
        public UserDto User { get; set; } = new UserDto();
    }
}
