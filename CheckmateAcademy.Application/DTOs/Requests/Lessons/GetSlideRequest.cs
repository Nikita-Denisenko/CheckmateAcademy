namespace CheckmateAcademy.Application.DTOs.Requests.Lessons
{
    public record GetSlideRequest
    {
        public int LessonId { get; init; }
        public int UserId { get; init; }
        public int Order { get; init; }

        public GetSlideRequest(int lessonId, int userId, int order)
        {
            if (lessonId <= 0)
                throw new ArgumentException("LessonId must be greater than zero.", nameof(lessonId));

            if (userId <= 0)
                throw new ArgumentException("UserId must be greater than zero.", nameof(userId));

            if (order <= 0)
                throw new ArgumentException("Order must be greater than zero.", nameof(order));

            LessonId = lessonId;
            UserId = userId;
            Order = order;
        }
    }
}
