namespace CheckmateAcademy.Application.DTOs.Requests.Courses
{
    public record EnrollToCourseRequest
    {
        public int CourseId { get; init; }
        public int UserId { get; init; }
    }
}
