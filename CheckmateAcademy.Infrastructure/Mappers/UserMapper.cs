using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Domain.ValueObjects.Profile;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static User ToDomain(UserData data)
        {
            var profile = new UserProfile(
                data.Name,
                data.Surname,
                data.BirthDate,
                data.AboutInfo,
                data.AvatarUrl,
                data.FideRating,
                data.RusRating);

            var account = new UserAccount(
                new Email(data.Email),
                data.PasswordHash);

            return new User(
                data.Id,
                profile,
                account);
        }

        public static UserData ToData(User user)
        {
            return new UserData(
                user.Id,
                user.Profile.Name,
                user.Profile.Surname,
                user.Profile.BirthDate,
                user.Profile.AboutInfo,
                user.Profile.AvatarUrl,
                user.Profile.FideRating,
                user.Profile.RusRating,
                user.Account.Email.Value,
                user.Account.PasswordHash);
        }
    }
}
