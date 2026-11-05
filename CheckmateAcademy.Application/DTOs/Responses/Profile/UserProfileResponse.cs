namespace CheckmateAcademy.Application.DTOs.Responses.Profile
{
    public record UserProfileResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public DateTime BirthDate { get; init; }
        public string? AboutInfo { get; init; }
        public string? AvatarUrl { get; init; }
        public int? FideRating { get; init; }
        public int? RusRating { get; init; }
        public int FollowersCount { get; init; }
        public int TotalPoints { get; init; }
        public int GamesQuantity { get; init; }
        public int StudiesQuantity { get; init; }

        public UserProfileResponse(
            int id,
            string name,
            string surname,
            DateTime birthDate,
            string? aboutInfo,
            string? avatarUrl,
            int? fideRating,
            int? rusRating,
            int followersCount,
            int totalPoints,
            int gamesQuantity,
            int studiesQuantity)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));

            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Surname cannot be null or whitespace.", nameof(surname));

            if (birthDate > DateTime.Now)
                throw new ArgumentOutOfRangeException(nameof(birthDate), "Birth date cannot be in the future.");

            if (followersCount < 0)
                throw new ArgumentException("Followers count cannot be negative.", nameof(followersCount));

            if (totalPoints < 0)
                throw new ArgumentException("Total points cannot be negative.", nameof(totalPoints));

            if (gamesQuantity < 0)
                throw new ArgumentException("Games quantity cannot be negative.", nameof(gamesQuantity));

            if (studiesQuantity < 0)
                throw new ArgumentException("Studies quantity cannot be negative.", nameof(studiesQuantity));

            Id = id;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            AboutInfo = aboutInfo;
            AvatarUrl = avatarUrl;
            FideRating = fideRating;
            RusRating = rusRating;
            FollowersCount = followersCount;
            TotalPoints = totalPoints;
            GamesQuantity = gamesQuantity;
            StudiesQuantity = studiesQuantity;
        }
    }
}
