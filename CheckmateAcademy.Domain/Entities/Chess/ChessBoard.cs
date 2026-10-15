using CheckmateAcademy.Domain.Common;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.Exceptions;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities.Chess
{
    public class ChessBoard
    {
        private readonly List<Piece> _pieces;

        public IReadOnlyList<Piece> Pieces => _pieces;

        public ChessBoard()
        {
            _pieces = StartPositionInitializer.InitializePieces();
        }

        public void MakeMove(Move move)
        {
            DomainValidation.NotNull(move, nameof(move));

            if (!ChessRulesService.IsValidMove(this, move))
                throw new DomainException("Invalid move.");

            Piece piece = move.Piece;

            if (move.Type == MoveType.Castling)
            {
                MoveCastlingPieces(move);
                return;
            }

            Piece? capturedPiece = _pieces.FirstOrDefault(
                p => p.Position == move.To);

            if (capturedPiece != null)
                _pieces.Remove(capturedPiece);

            piece.MoveTo(move.To);
        }

        private void MoveCastlingPieces(Move move)
        {
            Piece king = move.Piece;

            int row = move.From.Row;
            bool isShortCastling = move.To.Column > move.From.Column;

            int rookFromColumn = isShortCastling ? 8 : 1;
            int rookToColumn = isShortCastling ? 6 : 4;

            var rookPosition = new BoardPosition(row, rookFromColumn);
            var rookTargetPosition = new BoardPosition(row, rookToColumn);

            Piece? rook = _pieces.FirstOrDefault(
                p => p.Position == rookPosition);

            if (rook is not Rook)
                throw new DomainException("Castling rook was not found.");

            king.MoveTo(move.To);
            rook.MoveTo(rookTargetPosition);
        }
    }
}
