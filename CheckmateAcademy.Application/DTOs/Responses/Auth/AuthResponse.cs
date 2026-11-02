namespace CheckmateAcademy.Application.DTOs.Responses.Auth
{
    public record AuthResponse
    {
        public string Token { get; init; }

        public AuthResponse(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token cannot be null or empty.", nameof(token));

            Token = token;
        }
    }
}
