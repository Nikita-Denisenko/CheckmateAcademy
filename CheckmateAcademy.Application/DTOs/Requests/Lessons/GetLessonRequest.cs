namespace CheckmateAcademy.Application.DTOs.Requests.Lessons
{
    public record GetLessonRequest
    {
        public int Id { get; init; }
        public int UserId { get; init; }

        public GetLessonRequest(int id, int userId)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));
            if (userId <= 0)
                throw new ArgumentException("UserId must be greater than zero.", nameof(userId));
            Id = id;
            UserId = userId;
        }
    }
}
