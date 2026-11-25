namespace CheckmateAcademy.Application.DTOs.Responses.Auth
{
    public record AuthResponse
    {
        public int UserId { get; init; }
        public string Token { get; init; }

        public AuthResponse(int userId, string token)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token cannot be null or empty.", nameof(token));

            UserId = userId;
            Token = token;
        }
    }
}
