namespace CheckmateAcademy.Application.DTOs.Responses.Lessons
{
    public record LessonResponse
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int Order { get; init; }
        public int SlidesCount { get; init; }
        public int CompletedSlides { get; init; }
        public int ReceivedPoints { get; init; }
        public int TotalPoints { get; init; }
        public bool IsCompleted { get; init; }

        public LessonResponse(
            int id,
            string title,
            string description,
            int order,
            int slidesCount,
            int completedSlides,
            int receivedPoints,
            int totalPoints,
            bool isCompleted = false)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Id must be a positive integer.",
                    nameof(id));
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException(
                    "Title cannot be null or whitespace.",
                    nameof(title));
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException(
                    "Description cannot be null or whitespace.",
                    nameof(description));
            }
            if (order < 0)
            {
                throw new ArgumentException(
                    "Order must be a non-negative integer.",
                    nameof(order));
            }
            if (slidesCount <= 0)
            {
                throw new ArgumentException(
                    "SlidesCount must be a positive integer.",
                    nameof(slidesCount));
            }

            if (completedSlides < 0 || completedSlides > slidesCount)
            {
                throw new ArgumentException(
                    "CompletedSlides must be a non-negative integer and cannot exceed SlidesCount.",
                    nameof(completedSlides));
            }

            if (totalPoints < 0)
            {
                throw new ArgumentException(
                    "TotalPoints must be a non-negative integer.",
                    nameof(totalPoints));
            }

            if (receivedPoints < 0 || receivedPoints > totalPoints)
            {
                throw new ArgumentException(
                    "ReceivedPoints must be a non-negative integer and cannot exceed TotalPoints.",
                    nameof(receivedPoints));
            }

            Id = id;
            Title = title;
            Description = description;
            Order = order;
            SlidesCount = slidesCount;
            CompletedSlides = completedSlides;
            ReceivedPoints = receivedPoints;
            TotalPoints = totalPoints;
            IsCompleted = isCompleted;
        }
    }
}
