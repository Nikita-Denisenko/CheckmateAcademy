using CheckmateAcademy.Domain.Common;

namespace CheckmateAcademy.Domain.Entities
{
    public class Notification
    {
        public int Id { get; }
        public int UserId { get; }
        public string Message { get; }
        public DateTime CreatedAt { get; }
        public bool IsRead { get; private set; }

        public Notification(
            int id,
            int userId,
            string message)
        {
            DomainValidation.PositiveId(id, nameof(id));
            DomainValidation.PositiveId(userId, nameof(userId));
            DomainValidation.NotEmptyString(message, nameof(message));

            Id = id;
            UserId = userId;
            Message = message;
            CreatedAt = DateTime.UtcNow;
            IsRead = false;
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}