using CheckmateAcademy.Application.Interfaces.Services;
using CheckmateAcademy.Domain.Enums;

namespace CheckmateAcademy.Presentation.Services.Auth
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        public int? UserId { get; private set; }
        public Role? Role { get; private set; }

        public bool IsAuthenticated => UserId.HasValue;

        public void SetCurrentUser(int userId, Role role)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            UserId = userId;
            Role = role;
        }

        public void Clear()
        {
            UserId = null;
            Role = null;
        }
    }
}
