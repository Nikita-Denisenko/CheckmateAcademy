namespace CheckmateAcademy.Application.DTOs.Responses.Lessons
{
    public record LessonCardResponse
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public int Order { get; init; }
        public int SlidesCount { get; init; }
        public int CompletedSlides { get; init; }

        public LessonCardResponse(
            int id,
            string title,
            int order,
            int slidesCount,
            int completedSlides)
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

            Id = id;
            Title = title;
            Order = order;
            SlidesCount = slidesCount;
            CompletedSlides = completedSlides;
        }
    }
}
