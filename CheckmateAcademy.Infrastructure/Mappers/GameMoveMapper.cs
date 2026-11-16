
using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.Exceptions;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class GameMoveMapper
    {
        public static Move ToDomain(
            GameMoveData data,
            ChessBoard chessBoard)
        {
            var from = new BoardPosition(
                data.FromRow,
                data.FromColumn);

            var to = new BoardPosition(
                data.ToRow,
                data.ToColumn);

            var piece = chessBoard.Pieces
                .FirstOrDefault(piece => piece.Position == from)
                ?? throw new DomainException(
                    $"Piece not found at position {from}.");

            var type = Enum.Parse<MoveType>(data.Type, true);

            return new Move(piece, from, to, type);
        }

        public static GameMoveData ToData(Move move)
        {
            return new GameMoveData
            {
                FromRow = move.From.Row,
                FromColumn = move.From.Column,
                ToRow = move.To.Row,
                ToColumn = move.To.Column,
                Type = move.Type.ToString()
            };
        }
    }
}
