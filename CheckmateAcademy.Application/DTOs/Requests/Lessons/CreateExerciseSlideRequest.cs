namespace CheckmateAcademy.Application.DTOs.Requests.Lessons;

public record CreateExerciseSlideRequest : CreateLessonSlideRequest
{
    public CreateExerciseRequest Exercise { get; init; } = null!;
}
