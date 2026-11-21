namespace CheckmateAcademy.Application.DTOs.Requests.Lessons;

public abstract record CreateLessonSlideRequest
{
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
}
