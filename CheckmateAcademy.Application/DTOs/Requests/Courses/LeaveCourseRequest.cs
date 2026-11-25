namespace CheckmateAcademy.Application.DTOs.Requests.Courses
{
    public record LeaveCourseRequest
    {
        public int CourseId { get; init; }
        public int UserId { get; init; }
    }
}
