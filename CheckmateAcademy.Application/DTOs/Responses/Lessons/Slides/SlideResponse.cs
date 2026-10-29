namespace CheckmateAcademy.Application.DTOs.Responses.Lessons.Slides
{
    public record SlideResponse
    {
        public string Title { get; init; } = string.Empty;
        public int Order { get; init; }

    }
}
