using CheckmateAcademy.Domain.Enums;

namespace CheckmateAcademy.Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        Role? Role { get; }
        bool IsAuthenticated { get; }

        void SetCurrentUser(int userId, Role role);
        void Clear();
    }
}
