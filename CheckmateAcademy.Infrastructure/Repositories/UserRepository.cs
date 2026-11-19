using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task CreateUserAsync(User user, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByIdAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
