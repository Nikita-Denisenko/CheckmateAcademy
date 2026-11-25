namespace CheckmateAcademy.Application.DTOs.Requests.Account
{
    public record ChangePasswordRequest
    {
        public int UserId { get; init; }
        public string CurrentPassword { get; init; } = string.Empty;
        public string NewPassword { get; init; } = string.Empty;
    }
}
