namespace CheckmateAcademy.Application.DTOs.Requests.Courses
{
    public record GetCourseRequest
    {
        public int Id { get; init; }
        public int UserId { get; init; }

        public GetCourseRequest(int id, int userId)
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
