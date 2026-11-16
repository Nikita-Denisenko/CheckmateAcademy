using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.ValueObjects.Tasks;

public sealed record ExerciseSolutionMove
{
    public BoardPosition From { get; init; }
    public BoardPosition To { get; init; }
    public MoveType Type { get; init; } = MoveType.Normal;
}
