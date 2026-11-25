namespace CheckmateAcademy.Application.DTOs.Requests.Auth
{
    public record LoginRequest
    {
        public string Email { get; }
        public string Password { get; }

        public LoginRequest(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            Email = email;
            Password = password;
        }
    }
}
