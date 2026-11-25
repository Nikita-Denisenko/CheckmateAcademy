namespace CheckmateAcademy.Application.Services
{
    public sealed class JwtTokenOptions
    {
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public string SecretKey { get; init; } = string.Empty;
        public int ExpirationMinutes { get; init; } = 60;
    }
}
