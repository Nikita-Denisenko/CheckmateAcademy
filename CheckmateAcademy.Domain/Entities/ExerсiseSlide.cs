namespace CheckmateAcademy.Domain.Entities;

public class ExerciseSlide : LessonSlide
{
    public int ExerciseId { get; private set; }

    public Exercise Exercise { get; private set; }

    public ExerciseSlide(string title, int order, Exercise exercise)
        : base(title, order)
    {
        ArgumentNullException.ThrowIfNull(exercise);

        Exercise = exercise;
        ExerciseId = exercise.Id;
    }
}
