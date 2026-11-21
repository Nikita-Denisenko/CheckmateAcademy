using CheckmateAcademy.Domain.Enums;

namespace CheckmateAcademy.Domain.ValueObjects.Game;

public sealed record ExerciseSolutionMove
{
    public BoardPosition From { get; init; }
    public BoardPosition To { get; init; }
    public MoveType Type { get; init; } = MoveType.Normal;
}
