namespace CheckmateAcademy.Application.DTOs.Requests.Lessons.Blocks;

public record UpdateTextBlockRequest : UpdateContentBlockRequest
{
    public string Text { get; set; } = string.Empty;
}
