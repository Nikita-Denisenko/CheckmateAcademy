namespace CheckmateAcademy.Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        bool IsAuthenticated { get; }

        void SetCurrentUser(int userId);
        void Clear();
    }
}
