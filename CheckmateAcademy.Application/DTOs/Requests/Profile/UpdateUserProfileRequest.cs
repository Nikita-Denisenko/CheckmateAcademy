namespace CheckmateAcademy.Application.DTOs.Requests.Profile
{
    public record UpdateUserProfileRequest
    {
        public int Id { get; init; }
        public string Name { get; } 
        public string Surname { get; }
        public DateTime BirthDate { get; }
        public string? AboutInfo { get; }
        public string? AvatarUrl { get; }
        public int? FideRating { get; }
        public int? RusRating { get; }

        public UpdateUserProfileRequest(
            int id,
            string name,
            string surname,
            DateTime birthDate,
            string? aboutInfo = null,
            string? avatarUrl = null,
            int? fideRating = null,
            int? rusRating = null)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));

            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Surname cannot be null or whitespace.", nameof(surname));

            if (birthDate > DateTime.Now)
                throw new ArgumentOutOfRangeException(nameof(birthDate), "Birth date cannot be in the future.");

            Id = id;
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
