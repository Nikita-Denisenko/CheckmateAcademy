namespace CheckmateAcademy.Application.DTOs.Requests.Lessons
{
    public record CreateLessonRequest
    {
        public string Title { get; init; } = string.Empty;
        public List<CreateLessonSlideRequest> Slides { get; init; } = [];
    }
}
