using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.ValueObjects.Game;

public enum PieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public sealed record PieceSetup(
    PieceType Type,
    Color Color,
    BoardPosition Position);
