using CheckmateAcademy.Domain.Common;
using CheckmateAcademy.Domain.ValueObjects.Profile;

namespace CheckmateAcademy.Domain.Entities
{
    public class User
    {
        public int Id { get; }

        public UserProfile Profile { get; private set; }
        public UserAccount Account { get; private set; }

        public User(
            int id,
            UserProfile profile,
            UserAccount account)
        {
            DomainValidation.PositiveId(id, nameof(id));

            if (profile is null)
                throw new ArgumentNullException(nameof(profile));

            if (account is null)
                throw new ArgumentNullException(nameof(account));

            Id = id;
            Profile = profile;
            Account = account;
        }
    }
}
