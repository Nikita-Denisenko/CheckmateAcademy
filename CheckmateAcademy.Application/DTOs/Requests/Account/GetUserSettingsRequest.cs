namespace CheckmateAcademy.Application.DTOs.Requests.Account
{
    public record GetUserSettingsRequest
    {
        public int UserId { get; init; }
    }
}
