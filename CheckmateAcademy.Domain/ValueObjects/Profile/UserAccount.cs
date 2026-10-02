namespace CheckmateAcademy.Domain.ValueObjects.Profile
{
    public record UserAccount
    {
        public Email Email { get; } = null!;
        public string PasswordHash { get; } = null!;

        public UserAccount(Email email, string passwordHash)
        {
            Email = email 
                ?? throw new ArgumentNullException(nameof(email));

            PasswordHash = passwordHash 
                ?? throw new ArgumentNullException(nameof(passwordHash));
        }
    }
}
