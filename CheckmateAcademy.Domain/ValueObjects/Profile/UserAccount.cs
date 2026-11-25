using CheckmateAcademy.Domain.Enums;

namespace CheckmateAcademy.Domain.ValueObjects.Profile
{
    public record UserAccount
    {
        public Email Email { get; } = null!;
        public string PasswordHash { get; } = null!;
        public Role Role { get; } = Role.Customer;


        public UserAccount(Email email, string passwordHash, Role role)
        {
            Email = email 
                ?? throw new ArgumentNullException(nameof(email));

            PasswordHash = passwordHash 
                ?? throw new ArgumentNullException(nameof(passwordHash));

            Role = role;
        }
    }
}
