using CheckmateAcademy.Domain.Enums;

namespace CheckmateAcademy.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(int userId, Role role);
    }
}
