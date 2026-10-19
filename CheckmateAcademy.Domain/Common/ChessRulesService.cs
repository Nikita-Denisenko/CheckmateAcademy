using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.Exceptions;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Common
{
    public static class ChessRulesService
    {
        public static bool IsValidMove(ChessBoard board, Move move)
        {
            ArgumentNullException.ThrowIfNull(board);
            ArgumentNullException.ThrowIfNull(move);

            Piece piece = move.Piece;

            if (!board.Pieces.Contains(piece))
                return false;

            if (piece.Position != move.From)
                return false;

            if (move.From == move.To)
                return false;

            Piece? targetPiece = FindPieceAt(board, move.To);

            if (targetPiece != null &&
                targetPiece.Color == piece.Color)
            {
                return false;
            }

            if (move.Type == MoveType.Castling)
            {
                return IsValidCastling(board, move);
            }

            if (move.Type != MoveType.Normal)
                return false;

            if (piece is King &&
                Math.Abs(move.To.Column - move.From.Column) == 2)
            {
                return false;
            }

            if (!IsValidPieceMovement(board, piece, move.To, targetPiece))
                return false;

            return DoesNotExposeKing(board, piece, move.To, targetPiece);
        }

        private static bool IsValidPieceMovement(
            ChessBoard board,
            Piece piece,
            BoardPosition target,
            Piece? targetPiece)
        {
            if (piece is Pawn pawn)
                return IsValidPawnMove(board, pawn, target, targetPiece);

            if (!piece.IsValidMove(target))
                return false;

            if (piece is Knight)
                return true;

            return !IsPathBlocked(board, piece.Position, target);
        }

        private static bool IsValidPawnMove(
            ChessBoard board,
            Pawn pawn,
            BoardPosition target,
            Piece? targetPiece)
        {
            int direction = pawn.Color == Color.White ? -1 : 1;

            int rowDifference = target.Row - pawn.Position.Row;
            int columnDifference = target.Column - pawn.Position.Column;

            if (columnDifference == 0 &&
                rowDifference == direction &&
                targetPiece == null)
            {
                return true;
            }

            int startingRow = pawn.Color == Color.White ? 7 : 2;

            if (columnDifference == 0 &&
                pawn.Position.Row == startingRow &&
                rowDifference == 2 * direction &&
                targetPiece == null)
            {
                var intermediatePosition = new BoardPosition(
                    pawn.Position.Row + direction,
                    pawn.Position.Column);

                return FindPieceAt(board, intermediatePosition) == null;
            }

            if (Math.Abs(columnDifference) == 1 &&
                rowDifference == direction &&
                targetPiece != null &&
                targetPiece.Color != pawn.Color)
            {
                return true;
            }

            return IsValidEnPassant(board, pawn, target, targetPiece);
        }

        private static bool IsValidCastling(ChessBoard board, Move move)
        {
            if (move.Piece is not King king)
                return false;

            int row = king.Color == Color.White ? 8 : 1;

            if (king.Position != new BoardPosition(row, 5))
                return false;

            if (move.From != king.Position || move.From.Row != row)
                return false;

            if (king.HasMoved)
                return false;

            int columnDifference = move.To.Column - move.From.Column;

            if (Math.Abs(columnDifference) != 2 ||
                move.To.Row != row)
            {
                return false;
            }

            bool isShortCastling = columnDifference > 0;

            int rookColumn = isShortCastling ? 8 : 1;
            int rookTargetColumn = isShortCastling ? 6 : 4;

            var rookPosition = new BoardPosition(row, rookColumn);
            var rookTargetPosition = new BoardPosition(row, rookTargetColumn);

            if (FindPieceAt(board, rookPosition) is not Rook rook)
                return false;

            if (rook.Color != king.Color || rook.HasMoved)
                return false;

            int startColumn = isShortCastling ? 6 : 2;
            int endColumn = isShortCastling ? 7 : 4;

            for (int column = startColumn; column <= endColumn; column++)
            {
                if (FindPieceAt(board, new BoardPosition(row, column)) != null)
                    return false;
            }

            if (IsSquareAttacked(board, king.Position, Opponent(king.Color)))
                return false;

            int transitColumn = isShortCastling ? 6 : 4;

            var transitPosition = new BoardPosition(row, transitColumn);

            if (IsSquareAttacked(board, transitPosition, Opponent(king.Color)))
                return false;

            if (IsSquareAttacked(board, move.To, Opponent(king.Color)))
                return false;

            king.SetPositionForSimulation(move.To);
            rook.SetPositionForSimulation(rookTargetPosition);

            bool kingIsSafe = !IsKingInCheck(board, king.Color);

            king.SetPositionForSimulation(move.From);
            rook.SetPositionForSimulation(rookPosition);

            return kingIsSafe;
        }

        public static bool IsKingInCheck(ChessBoard board, Color color)
        {
            ArgumentNullException.ThrowIfNull(board);

            King? king = board.Pieces
                .OfType<King>()
                .FirstOrDefault(piece => piece.Color == color);

            if (king == null)
                throw new DomainException(
                    $"The {color} king was not found on the board.");

            return IsSquareAttacked(board, king.Position, Opponent(color));
        }

        private static bool IsSquareAttacked(
            ChessBoard board,
            BoardPosition target,
            Color attackingColor)
        {
            foreach (Piece piece in board.Pieces)
            {
                if (piece.Color != attackingColor)
                    continue;

                if (piece is Pawn pawn)
                {
                    int direction = pawn.Color == Color.White ? -1 : 1;

                    if (target.Row - pawn.Position.Row == direction &&
                        Math.Abs(target.Column - pawn.Position.Column) == 1)
                    {
                        return true;
                    }

                    continue;
                }

                int rowDifference = target.Row - piece.Position.Row;
                int columnDifference = target.Column - piece.Position.Column;

                if (piece is King &&
                    Math.Abs(rowDifference) <= 1 &&
                    Math.Abs(columnDifference) <= 1 &&
                    (rowDifference != 0 || columnDifference != 0))
                {
                    return true;
                }

                if (!piece.IsValidMove(target))
                    continue;

                if (piece is Knight ||
                    !IsPathBlocked(board, piece.Position, target))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool DoesNotExposeKing(
            ChessBoard board,
            Piece piece,
            BoardPosition target,
            Piece? capturedPiece)
        {
            BoardPosition originalPosition = piece.Position;

            piece.SetPositionForSimulation(target);

            bool kingIsSafe;

            try
            {
                kingIsSafe = !IsKingInCheck(board, piece.Color, capturedPiece);
            }
            finally
            {
                piece.SetPositionForSimulation(originalPosition);
            }

            return kingIsSafe;
        }

        private static bool IsKingInCheck(
            ChessBoard board,
            Color color,
            Piece? ignoredPiece)
        {
            King? king = board.Pieces
                .OfType<King>()
                .FirstOrDefault(piece => piece.Color == color);

            if (king == null)
                throw new DomainException(
                    $"The {color} king was not found on the board.");

            foreach (Piece attacker in board.Pieces)
            {
                if (attacker == ignoredPiece || attacker.Color == color)
                    continue;

                if (attacker is Pawn pawn)
                {
                    int direction = pawn.Color == Color.White ? -1 : 1;

                    if (king.Position.Row - pawn.Position.Row == direction &&
                        Math.Abs(king.Position.Column - pawn.Position.Column) == 1)
                    {
                        return true;
                    }

                    continue;
                }

                int rowDifference = king.Position.Row - attacker.Position.Row;
                int columnDifference = king.Position.Column - attacker.Position.Column;

                if (attacker is King &&
                    Math.Abs(rowDifference) <= 1 &&
                    Math.Abs(columnDifference) <= 1 &&
                    (rowDifference != 0 || columnDifference != 0))
                {
                    return true;
                }

                if (!attacker.IsValidMove(king.Position))
                    continue;

                if (attacker is Knight ||
                    !IsPathBlocked(board, attacker.Position, king.Position))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsPathBlocked(
            ChessBoard board,
            BoardPosition from,
            BoardPosition to)
        {
            int rowDifference = to.Row - from.Row;
            int columnDifference = to.Column - from.Column;

            int rowStep = Math.Sign(rowDifference);
            int columnStep = Math.Sign(columnDifference);

            int row = from.Row + rowStep;
            int column = from.Column + columnStep;

            while (row != to.Row || column != to.Column)
            {
                if (FindPieceAt(board, new BoardPosition(row, column)) != null)
                    return true;

                row += rowStep;
                column += columnStep;
            }

            return false;
        }

        private static Piece? FindPieceAt(
            ChessBoard board,
            BoardPosition position)
        {
            return board.Pieces.FirstOrDefault(
                piece => piece.Position == position);
        }

        private static Color Opponent(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        private static bool IsValidEnPassant(
            ChessBoard board,
            Pawn pawn,
            BoardPosition target,
            Piece? targetPiece)
        {
            if (targetPiece != null)
                return false;

            int direction = pawn.Color == Color.White ? -1 : 1;

            int rowDifference = target.Row - pawn.Position.Row;
            int columnDifference = target.Column - pawn.Position.Column;

            if (rowDifference != direction ||
                Math.Abs(columnDifference) != 1)
            {
                return false;
            }

            Move? lastMove = board.LastMove;

            if (lastMove == null ||
                lastMove.Type != MoveType.Normal ||
                lastMove.Piece is not Pawn opponentPawn)
            {
                return false;
            }

            if (opponentPawn.Color == pawn.Color)
                return false;

            if (lastMove.From.Column != lastMove.To.Column)
                return false;

            if (Math.Abs(lastMove.To.Row - lastMove.From.Row) != 2)
                return false;

            if (lastMove.To != opponentPawn.Position)
                return false;

            if (opponentPawn.Position.Row != pawn.Position.Row)
                return false;

            if (opponentPawn.Position.Column != target.Column)
                return false;

            return board.Pieces.Contains(opponentPawn);
        }
    }
}
