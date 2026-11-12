using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Enums;

namespace CheckmateAcademy.Domain.Entities;

public class UserExerciseProgress
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; }
    public ChessBoard CurrentBoard { get; private set; }

    public int AttemptsCount { get; private set; }

    public ExerciseProgressStatus Status { get; private set; }

    public DateTime StartedAt { get; private set; }

    public DateTime? CompletedAt { get; private set; }

    protected UserExerciseProgress()
    {
        Exercise = null!;
        CurrentBoard = null!;
    }

    public UserExerciseProgress(int userId, Exercise exercise)
    {
        ArgumentNullException.ThrowIfNull(exercise);

        UserId = userId;
        Exercise = exercise;
        ExerciseId = exercise.Id;

        CurrentBoard = new ChessBoard(exercise.StartPosition);

        AttemptsCount = 1;
        Status = ExerciseProgressStatus.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void StartNewAttempt()
    {
        CurrentBoard = new ChessBoard(Exercise.StartPosition);

        AttemptsCount++;
        Status = ExerciseProgressStatus.InProgress;
        CompletedAt = null;
    }

    public void Complete()
    {
        if (Status == ExerciseProgressStatus.Completed)
            return;

        Status = ExerciseProgressStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }
}
