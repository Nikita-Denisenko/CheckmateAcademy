namespace CheckmateAcademy.Application.DTOs.Requests.Account
{
    public record ChangeEmailRequest
    {
        public int UserId { get; init; }
        public string NewEmail { get; init; } = string.Empty;
    }
}
