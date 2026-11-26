using CheckmateAcademy.Domain.Enums;

namespace CheckmateAcademy.Application.DTOs.Responses.Auth
{
    public record AuthResponse
    {
        public int UserId { get; init; }
        public Role Role { get; init; }

        public AuthResponse(int userId, Role role)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            UserId = userId;
            Role = role;
        }
    }
}

