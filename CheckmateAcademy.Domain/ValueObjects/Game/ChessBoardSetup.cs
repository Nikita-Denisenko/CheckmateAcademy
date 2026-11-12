namespace CheckmateAcademy.Domain.ValueObjects.Game;

public sealed class ChessBoardSetup
{
    public IReadOnlyList<PieceSetup> Pieces { get; }

    public ChessBoardSetup(IEnumerable<PieceSetup> pieces)
    {
        ArgumentNullException.ThrowIfNull(pieces);

        var setup = pieces.ToList();

        if (setup.GroupBy(p => p.Position).Any(g => g.Count() > 1))
            throw new ArgumentException("Multiple pieces cannot occupy the same position.");

        Pieces = setup.AsReadOnly();
    }
}
