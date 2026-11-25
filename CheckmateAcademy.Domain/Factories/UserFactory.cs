using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Profile;

namespace CheckmateAcademy.Domain.Factories
{
    public static class UserFactory
    {
        public static User CreateUser(
            int id,
            string name,
            string surname,
            DateTime birthDate,
            string email,
            string passwordHash,
            Role role,
            string? aboutInfo = null,
            string? avatarUrl = null,
            int? fideRating = null,
            int? rusRating = null)
        {
            var profile = new UserProfile(
                name,
                surname,
                birthDate,
                aboutInfo,
                avatarUrl,
                fideRating,
                rusRating);

            var account = new UserAccount(
                new Email(email),
                passwordHash,
                role);

            return new User(id, profile, account);
        }
    }
}
