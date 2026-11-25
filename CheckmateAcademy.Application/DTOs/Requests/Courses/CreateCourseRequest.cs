using CheckmateAcademy.Application.DTOs.Requests.Lessons;

namespace CheckmateAcademy.Application.DTOs.Requests.Courses
{
    public record CreateCourseRequest
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public List<CreateLessonRequest> Lessons { get; init; } = [];
    }
}
