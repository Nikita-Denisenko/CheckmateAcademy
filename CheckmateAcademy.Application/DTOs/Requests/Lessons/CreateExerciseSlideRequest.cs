using CheckmateAcademy.Application.DTOs.Requests.Lessons.Slides;

namespace CheckmateAcademy.Application.DTOs.Requests.Lessons;

public record CreateExerciseSlideRequest : CreateLessonSlideRequest
{
    public CreateExerciseRequest Exercise { get; set; } = null!;
}
