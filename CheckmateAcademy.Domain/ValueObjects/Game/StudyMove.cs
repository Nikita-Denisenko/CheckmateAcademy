using CheckmateAcademy.Domain.Enums;

namespace CheckmateAcademy.Domain.ValueObjects.Game
{
    public sealed record StudyMove(
        BoardPosition From,
        BoardPosition To,
        MoveType Type,
        string Notation,
        string? Comment);
}
