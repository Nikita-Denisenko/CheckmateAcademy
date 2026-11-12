using CheckmateAcademy.Domain.ValueObjects;
using CheckmateAcademy.Domain.ValueObjects.Game;
using CheckmateAcademy.Domain.ValueObjects.Tasks;

namespace CheckmateAcademy.Domain.Entities;

public class Exercise
{
    private readonly List<ExerciseSolutionMove> _solution = [];
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public ChessBoardSetup StartPosition { get; private set; }

    public IReadOnlyCollection<ExerciseSolutionMove> Solution =>
        _solution.AsReadOnly();

    protected Exercise()
    {
        Title = null!;
        Description = null!;
        StartPosition = null!;
    }

    public Exercise(
        string title,
        string description,
        ChessBoardSetup startPosition,
        IEnumerable<ExerciseSolutionMove> solution)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentNullException.ThrowIfNull(startPosition);
        ArgumentNullException.ThrowIfNull(solution);

        Title = title;
        Description = description;
        StartPosition = startPosition;

        _solution.AddRange(solution);
    }
}
