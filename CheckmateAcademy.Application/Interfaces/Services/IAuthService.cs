
using CheckmateAcademy.Application.DTOs.Requests.Auth;
using CheckmateAcademy.Application.DTOs.Responses.Auth;

namespace CheckmateAcademy.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(
            RegisterRequest request,
            CancellationToken ct);

        Task<AuthResponse> LoginAsync(
            LoginRequest request,
            CancellationToken ct);

        void Logout();
    }
}
