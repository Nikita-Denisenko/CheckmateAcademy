namespace CheckmateAcademy.Application.DTOs.Responses.Profile
{
    public record UserSettingsResponse
    {
        public string Email { get; init; } = null!;
    }
}
