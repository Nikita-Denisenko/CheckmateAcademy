using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        public Task CreateNotificationAsync(Notification notification, CancellationToken ct);
        public Task<Notification?> GetNotificationByIdAsync(int id, CancellationToken ct);
        public Task<List<Notification>> GetAllNotificationsAsync(CancellationToken ct);
        public Task UpdateNotificationAsync(Notification notification, CancellationToken ct);
        public Task DeleteNotificationAsync(int id, CancellationToken ct);
    }
}
