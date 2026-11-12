using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Enums;

namespace CheckmateAcademy.Domain.Entities;

public class UserTaskProgress
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int TaskId { get; private set; }
    public Task Task { get; private set; }
    public ChessBoard CurrentBoard { get; private set; }
    public int AttemptsCount { get; private set; }
    public TaskProgressStatus Status { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    protected UserTaskProgress()
    {
        Task = null!;
        CurrentBoard = null!;
    }

    public UserTaskProgress(int userId, Task task)
    {
        ArgumentNullException.ThrowIfNull(task);

        UserId = userId;
        Task = task;
        TaskId = task.Id;

        CurrentBoard = new ChessBoard(task.StartPosition);

        AttemptsCount = 1;
        Status = TaskProgressStatus.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void StartNewAttempt()
    {
        CurrentBoard = new ChessBoard(Task.StartPosition);

        AttemptsCount++;
        Status = TaskProgressStatus.InProgress;
        CompletedAt = null;
    }

    public void Complete()
    {
        if (Status == TaskProgressStatus.Completed)
            return;

        Status = TaskProgressStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }
}
