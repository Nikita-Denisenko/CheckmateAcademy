using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Domain.ValueObjects.Profile;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public UserRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateUserAsync(User user, CancellationToken ct)
        {
            EnsureFileExists();

            var userData = UserMapper.ToData(user);
            var json = JsonSerializer.Serialize(userData, JsonOptions);

            await File.AppendAllTextAsync(
                _storageOptions.UsersFilePath,
                json + Environment.NewLine,
                ct);
        }

        public async Task<User?> GetUserByIdAsync(int id, CancellationToken ct)
        {
            var users = await GetAllUsersAsync(ct);

            return users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<List<User>> GetAllUsersAsync(CancellationToken ct)
        {
            EnsureFileExists();

            var lines = await File.ReadAllLinesAsync(
                _storageOptions.UsersFilePath,
                ct);

            var users = new List<User>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var userData = JsonSerializer.Deserialize<UserData>(
                    line,
                    JsonOptions);

                if (userData is null)
                    continue;

                users.Add(UserMapper.ToDomain(userData));
            }

            return users;
        }

        public async Task UpdateUserAsync(User user, CancellationToken ct)
        {
            var users = await GetAllUsersAsync(ct);

            var index = users.FindIndex(u => u.Id == user.Id);

            if (index == -1)
                return;

            users[index] = user;

            await WriteUsersAsync(users, ct);
        }

        public async Task DeleteUserAsync(int id, CancellationToken ct)
        {
            var users = await GetAllUsersAsync(ct);

            var removedCount = users.RemoveAll(u => u.Id == id);

            if (removedCount == 0)
                return;

            await WriteUsersAsync(users, ct);
        }

        private async Task WriteUsersAsync(
            IEnumerable<User> users,
            CancellationToken ct)
        {
            var lines = users.Select(user =>
                JsonSerializer.Serialize(
                    UserMapper.ToData(user),
                    JsonOptions));

            await File.WriteAllLinesAsync(
                _storageOptions.UsersFilePath,
                lines,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.UsersFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
            {
                using var stream = File.Create(filePath);
            }
        }

        public async Task<User?> GetUserByEmailAsync(
            Email email,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(email);

            var users = await GetAllUsersAsync(ct);

            return users.FirstOrDefault(user =>
                string.Equals(
                    user.Account.Email.Value,
                    email.Value,
                    StringComparison.OrdinalIgnoreCase));
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllUsersAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
