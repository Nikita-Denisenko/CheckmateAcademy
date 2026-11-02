namespace CheckmateAcademy.Application.DTOs.Responses.Lessons.Slides
{
    public abstract record SlideResponse
    {
        public string Title { get; init; } = string.Empty;
        public int Order { get; init; }
        public DateTime CreatedAt { get; init; }

        public SlideResponse(string title, int order, DateTime createdAt)
        {
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
            Title = title;
            Order = order;
            CreatedAt = createdAt;
        }
    }
}
