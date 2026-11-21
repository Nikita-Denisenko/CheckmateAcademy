namespace CheckmateAcademy.Application.DTOs.Requests.Lessons
{
    public record UpdateLessonInfoRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
