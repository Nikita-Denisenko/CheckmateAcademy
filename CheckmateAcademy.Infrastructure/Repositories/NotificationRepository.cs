using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public Task CreateNotificationAsync(Notification notification, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNotificationAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetAllNotificationsAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Notification?> GetNotificationByIdAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNotificationAsync(Notification notification, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
