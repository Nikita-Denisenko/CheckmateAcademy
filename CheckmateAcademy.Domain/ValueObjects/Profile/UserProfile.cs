namespace CheckmateAcademy.Domain.ValueObjects.Profile
{
    public record UserProfile
    {
        public string Name { get; } = string.Empty;
        public string Surname { get; } = string.Empty;
        public DateTime BirthDate { get; }
        public string? AboutInfo { get; }
        public string? AvatarUrl { get; }
        public int? FideRating { get; }
        public int? RusRating { get; }

        public UserProfile(
            string name, 
            string surname, 
            DateTime birthDate,
            string? aboutInfo = null, 
            string? avatarUrl = null, 
            int? fideRating = null, 
            int? rusRating = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));

            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Surname cannot be null or whitespace.", nameof(surname));

            if (birthDate > DateTime.Now)
                throw new ArgumentOutOfRangeException(nameof(birthDate), "Birth date cannot be in the future.");
 
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            AboutInfo = aboutInfo;
            AvatarUrl = avatarUrl;
            FideRating = fideRating;
            RusRating = rusRating;
        }
    }
}
