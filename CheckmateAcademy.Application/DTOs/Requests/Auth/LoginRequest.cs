namespace CheckmateAcademy.Application.DTOs.Requests.Auth
{
    public record LoginRequest
    {
        public string Email { get; init; }
        public string PasswordHash { get; init; }

        public LoginRequest(string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash cannot be null or empty.", nameof(passwordHash));

            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
