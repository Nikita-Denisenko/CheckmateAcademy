using CheckmateAcademy.Domain.ValueObjects;
using CheckmateAcademy.Domain.ValueObjects.Game;
using CheckmateAcademy.Domain.ValueObjects.Tasks;

namespace CheckmateAcademy.Domain.Entities;

public class Task
{
    private readonly List<ExerciseSolutionMove> _solution = [];

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public ChessBoardSetup StartPosition { get; private set; }

    public IReadOnlyCollection<ExerciseSolutionMove> Solution =>
        _solution.AsReadOnly();

    public int Difficulty { get; private set; }

    protected Task()
    {
        Title = null!;
        Description = null!;
        StartPosition = null!;
    }

    public Task(
        string title,
        string description,
        ChessBoardSetup startPosition,
        IEnumerable<ExerciseSolutionMove> solution,
        int difficulty)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentNullException.ThrowIfNull(startPosition);
        ArgumentNullException.ThrowIfNull(solution);

        if (difficulty < 1 || difficulty > 5)
            throw new ArgumentOutOfRangeException(nameof(difficulty));

        Title = title;
        Description = description;
        StartPosition = startPosition;
        Difficulty = difficulty;

        _solution.AddRange(solution);

        if (_solution.Count == 0)
            throw new ArgumentException("Task solution cannot be empty.");
    }
}
