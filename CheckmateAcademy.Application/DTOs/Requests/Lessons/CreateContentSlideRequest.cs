using CheckmateAcademy.Application.DTOs.Requests.Lessons.Blocks;
using CheckmateAcademy.Application.DTOs.Requests.Lessons.Slides;

namespace CheckmateAcademy.Application.DTOs.Requests.Lessons;

public record CreateContentSlideRequest : CreateLessonSlideRequest
{
    public List<CreateContentBlockRequest> Blocks { get; set; } = [];
}