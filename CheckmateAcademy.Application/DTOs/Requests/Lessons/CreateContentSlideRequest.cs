using CheckmateAcademy.Application.DTOs.Requests.Lessons.Blocks;

namespace CheckmateAcademy.Application.DTOs.Requests.Lessons;

public record CreateContentSlideRequest : CreateLessonSlideRequest
{
    public List<CreateContentBlockRequest> Blocks { get; init; } = [];
}
