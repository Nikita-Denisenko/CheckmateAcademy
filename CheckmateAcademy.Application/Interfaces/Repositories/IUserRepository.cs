using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Domain.ValueObjects.Profile;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(User user, CancellationToken ct);
        public Task<User?> GetUserByIdAsync(int id, CancellationToken ct);
        public Task<List<User>> GetAllUsersAsync(CancellationToken ct);
        public Task UpdateUserAsync(User user, CancellationToken ct);
        Task<User?> GetUserByEmailAsync(Email email, CancellationToken ct);
        public Task DeleteUserAsync(int id, CancellationToken ct);
        public Task<int> GetNextIdAsync(CancellationToken ct);
    }
}
