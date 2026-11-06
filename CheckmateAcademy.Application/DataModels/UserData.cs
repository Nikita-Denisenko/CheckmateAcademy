namespace CheckmateAcademy.Application.DataModels
{
    public record UserData
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public DateTime BirthDate { get; init; }
        public string? AboutInfo { get; init; }
        public string? AvatarUrl { get; init; }
        public int? FideRating { get; init; }
        public int? RusRating { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }

        public UserData(
            int id,
            string name,
            string surname,
            DateTime birthDate,
            string? aboutInfo,
            string? avatarUrl,
            int? fideRating,
            int? rusRating,
            string email,
            string passwordHash)
        {
            Id = id;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            AboutInfo = aboutInfo;
            AvatarUrl = avatarUrl;
            FideRating = fideRating;
            RusRating = rusRating;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
