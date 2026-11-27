using CheckmateAcademy.Application.DTOs.Requests.Account;
using CheckmateAcademy.Application.DTOs.Requests.Profile;
using CheckmateAcademy.Application.DTOs.Responses.Profile;
namespace CheckmateAcademy.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserProfileResponse> GetUserProfileAsync(GetUserProfileRequest request, CancellationToken ct);
        Task<UserProfileResponse> UpdateUserProfileAsync(UpdateUserProfileRequest request, CancellationToken ct);
        Task DeleteAccoutAsync(DeleteAccountRequest request, CancellationToken ct);
        Task<UserSettingsResponse> GetUserSettingsResponseAsync(GetUserSettingsRequest request, CancellationToken ct);
        Task ChangeEmailAsync(ChangeEmailRequest request, CancellationToken ct);
        Task ChangePaswordAsync(ChangePasswordRequest request, CancellationToken ct);
    }
}
 