namespace CheckmateAcademy.Application.DataModels
{
    public record NotificationData
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public string Message { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public bool IsRead { get; init; }
    }
}
