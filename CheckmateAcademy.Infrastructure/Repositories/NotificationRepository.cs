using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public NotificationRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateNotificationAsync(
            Notification notification,
            CancellationToken ct)
        {
            EnsureFileExists();

            var notificationData = NotificationMapper.ToData(notification);
            var json = JsonSerializer.Serialize(notificationData, JsonOptions);

            await File.AppendAllTextAsync(
                _storageOptions.NotificationsFilePath,
                json + Environment.NewLine,
                ct);
        }

        public async Task DeleteNotificationAsync(int id, CancellationToken ct)
        {
            var notifications = await GetAllNotificationsAsync(ct);

            var removedCount = notifications.RemoveAll(n => n.Id == id);

            if (removedCount == 0)
                return;

            await WriteNotificationsAsync(notifications, ct);
        }

        public async Task<List<Notification>> GetAllNotificationsAsync(
            CancellationToken ct)
        {
            EnsureFileExists();

            var lines = await File.ReadAllLinesAsync(
                _storageOptions.NotificationsFilePath,
                ct);

            var notifications = new List<Notification>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var notificationData = JsonSerializer.Deserialize<NotificationData>(
                    line,
                    JsonOptions);

                if (notificationData is null)
                    continue;

                notifications.Add(NotificationMapper.ToDomain(notificationData));
            }

            return notifications;
        }

        public async Task<Notification?> GetNotificationByIdAsync(
            int id,
            CancellationToken ct)
        {
            var notifications = await GetAllNotificationsAsync(ct);

            return notifications.FirstOrDefault(n => n.Id == id);
        }

        public async Task UpdateNotificationAsync(
            Notification notification,
            CancellationToken ct)
        {
            var notifications = await GetAllNotificationsAsync(ct);

            var index = notifications.FindIndex(n => n.Id == notification.Id);

            if (index == -1)
                return;

            notifications[index] = notification;

            await WriteNotificationsAsync(notifications, ct);
        }

        private async Task WriteNotificationsAsync(
            IEnumerable<Notification> notifications,
            CancellationToken ct)
        {
            var lines = notifications.Select(notification =>
                JsonSerializer.Serialize(
                    NotificationMapper.ToData(notification),
                    JsonOptions));

            await File.WriteAllLinesAsync(
                _storageOptions.NotificationsFilePath,
                lines,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.NotificationsFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
            {
                using var stream = File.Create(filePath);
            }
        }
    }
}
