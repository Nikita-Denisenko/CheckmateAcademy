namespace CheckmateAcademy.Application.DTOs.Requests.Courses
{
    public record UpdateCourseInfoRequest
    {
        public int CourseId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }
}
