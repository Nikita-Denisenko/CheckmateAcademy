using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class NotificationMapper
    {
        public static NotificationData ToData(Notification notification)
        {
            return new NotificationData
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Message = notification.Message,
                CreatedAt = notification.CreatedAt,
                IsRead = notification.IsRead
            };
        }

        public static Notification ToDomain(NotificationData notificationData)
        {
            return new Notification(
                notificationData.Id,
                notificationData.UserId,
                notificationData.Message,
                notificationData.CreatedAt,
                notificationData.IsRead);
        }
    }
}
