using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class GameMoveMapper
    {
        public static Move ToDomain(GameMoveData data, ChessBoard board)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(board);

            var from = new BoardPosition(data.FromRow, data.FromColumn);
            var to = new BoardPosition(data.ToRow, data.ToColumn);

            var pieceType = Enum.Parse<PieceType>(
                data.PieceType,
                ignoreCase: true);

            var pieceColor = Enum.Parse<Color>(
                data.PieceColor,
                ignoreCase: true);

            var piece = board.Pieces.FirstOrDefault(p =>
                p.Position == from &&
                p.Color == pieceColor &&
                Enum.Parse<PieceType>(p.GetType().Name) == pieceType);

            if (piece == null)
            {
                throw new InvalidOperationException(
                    $"Piece {pieceColor} {pieceType} was not found at {from}.");
            }

            var moveType = Enum.Parse<MoveType>(
                data.Type,
                ignoreCase: true);

            return new Move(piece, from, to, moveType);
        }

        public static GameMoveData ToData(Move move)
        {
            ArgumentNullException.ThrowIfNull(move);

            return new GameMoveData
            {
                FromRow = move.From.Row,
                FromColumn = move.From.Column,
                ToRow = move.To.Row,
                ToColumn = move.To.Column,
                Type = move.Type.ToString(),
                PieceType = move.Piece.GetType().Name,
                PieceColor = move.Piece.Color.ToString()
            };
        }
    }
}
