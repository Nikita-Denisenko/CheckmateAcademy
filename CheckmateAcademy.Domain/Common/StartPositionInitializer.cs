using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Common
{
    public static class StartPositionInitializer
    {
        public static List<Piece> InitializePieces()
        {
            var pieces = new List<Piece>(32);

            for (int column = 1; column <= 8; column++)
            {
                pieces.Add(new Pawn(
                    Color.Black,
                    new BoardPosition(2, column)));

                pieces.Add(new Pawn(
                    Color.White,
                    new BoardPosition(7, column)));
            }

            for (int column = 1; column <= 8; column++)
            {
                pieces.Add(CreateBackRankPiece(
                    Color.Black,
                    new BoardPosition(1, column)));

                pieces.Add(CreateBackRankPiece(
                    Color.White,
                    new BoardPosition(8, column)));
            }

            return pieces;
        }

        private static Piece CreateBackRankPiece(
            Color color,
            BoardPosition position)
        {
            return position.Column switch
            {
                1 or 8 => new Rook(color, position),
                2 or 7 => new Knight(color, position),
                3 or 6 => new Bishop(color, position),
                4 => new Queen(color, position),
                5 => new King(color, position),
                _ => throw new ArgumentOutOfRangeException(
                    nameof(position),
                    "Invalid column for a chess piece.")
            };
        }
    }
}