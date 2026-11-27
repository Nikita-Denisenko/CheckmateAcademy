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
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
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
            var notifications = await GetAllNotificationsAsync(ct);
            notifications.Add(notification);
            await WriteNotificationsAsync(notifications, ct);
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

            var json = await File.ReadAllTextAsync(
                _storageOptions.NotificationsFilePath,
                ct);

            var notificationsData = JsonSerializer.Deserialize<List<NotificationData>>(
                json,
                JsonOptions) ?? [];

            return notificationsData
                .Select(NotificationMapper.ToDomain)
                .ToList();
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
            var notificationsData = notifications
                .Select(NotificationMapper.ToData)
                .ToList();

            var json = JsonSerializer.Serialize(notificationsData, JsonOptions);

            await File.WriteAllTextAsync(
                _storageOptions.NotificationsFilePath,
                json,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.NotificationsFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllNotificationsAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
