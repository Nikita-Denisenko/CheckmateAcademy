using CheckmateAcademy.Domain.Enums;

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

public sealed record PieceSetup
{
    public PieceType Type { get; init; }
    public Color Color { get; init; }
    public BoardPosition Position { get; init; }
    public bool HasMoved { get; init; }
}
