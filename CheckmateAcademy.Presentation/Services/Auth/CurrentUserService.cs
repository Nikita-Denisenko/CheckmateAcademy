using CheckmateAcademy.Application.Interfaces.Services;

namespace CheckmateAcademy.Presentation.Services.Auth
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        public int? UserId { get; private set; }

        public bool IsAuthenticated => UserId.HasValue;

        public void SetCurrentUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            UserId = userId;
        }

        public void Clear()
        {
            UserId = null;
        }
    }
}
