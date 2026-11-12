using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.ValueObjects.Tasks;

public sealed record ExerciseSolutionMove(
    BoardPosition From,
    BoardPosition To,
    MoveType Type = MoveType.Normal);
