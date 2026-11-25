namespace CheckmateAcademy.Application.DTOs.Requests.Lessons;

public abstract record CreateLessonSlideRequest
{
    public string Title { get; init; } = string.Empty;
    public int Order { get; init; }
}
