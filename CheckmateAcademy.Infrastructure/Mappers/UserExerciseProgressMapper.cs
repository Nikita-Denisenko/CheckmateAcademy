using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class UserExerciseProgressMapper
    {
        public static UserExerciseProgressData ToData(
            UserExerciseProgress progress)
        {
            ArgumentNullException.ThrowIfNull(progress);

            return new UserExerciseProgressData
            {
                UserId = progress.UserId,
                ExerciseId = progress.ExerciseId,

                CurrentBoard = progress.CurrentBoard.Pieces
                    .Select(piece => new PieceSetupData
                    {
                        Type = piece.GetType().Name,
                        Color = piece.Color.ToString(),
                        Row = piece.Position.Row,
                        Column = piece.Position.Column,
                        HasMoved = piece.HasMoved
                    })
                    .ToList(),

                LastMove = progress.CurrentBoard.LastMove == null
                    ? null
                    : new GameMoveData
                    {
                        FromRow = progress.CurrentBoard.LastMove.From.Row,
                        FromColumn = progress.CurrentBoard.LastMove.From.Column,
                        ToRow = progress.CurrentBoard.LastMove.To.Row,
                        ToColumn = progress.CurrentBoard.LastMove.To.Column,
                        Type = progress.CurrentBoard.LastMove.Type.ToString(),
                        PieceType = progress.CurrentBoard.LastMove.Piece.GetType().Name,
                        PieceColor = progress.CurrentBoard.LastMove.Piece.Color.ToString()
                    },

                AttemptsCount = progress.AttemptsCount,
                Status = progress.Status.ToString(),
                StartedAt = progress.StartedAt,
                CompletedAt = progress.CompletedAt
            };
        }

        public static UserExerciseProgress ToDomain(
            UserExerciseProgressData data,
            Exercise exercise)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(exercise);

            if (data.ExerciseId != exercise.Id)
            {
                throw new ArgumentException(
                    "Exercise ID does not match progress data.",
                    nameof(exercise));
            }

            var pieces = data.CurrentBoard
                .Select(piece => new PieceSetup
                {
                    Type = Enum.Parse<PieceType>(
                        piece.Type,
                        ignoreCase: true),

                    Color = Enum.Parse<Color>(
                        piece.Color,
                        ignoreCase: true),

                    Position = new BoardPosition(
                        piece.Row,
                        piece.Column),

                    HasMoved = piece.HasMoved
                })
                .ToList();

            var setup = new ChessBoardSetup(pieces);

            BoardPosition? lastMoveFrom = null;
            BoardPosition? lastMoveTo = null;
            MoveType? lastMoveType = null;

            if (data.LastMove != null)
            {
                lastMoveFrom = new BoardPosition(
                    data.LastMove.FromRow,
                    data.LastMove.FromColumn);

                lastMoveTo = new BoardPosition(
                    data.LastMove.ToRow,
                    data.LastMove.ToColumn);

                lastMoveType = Enum.Parse<MoveType>(
                    data.LastMove.Type,
                    ignoreCase: true);

                var pieceType = Enum.Parse<PieceType>(
                    data.LastMove.PieceType,
                    ignoreCase: true);

                var pieceColor = Enum.Parse<Color>(
                    data.LastMove.PieceColor,
                    ignoreCase: true);

                bool pieceExists = pieces.Any(piece =>
                    piece.Position == lastMoveTo.Value &&
                    piece.Type == pieceType &&
                    piece.Color == pieceColor);

                if (!pieceExists)
                {
                    throw new InvalidOperationException(
                        "Last move piece does not match the current board.");
                }
            }

            ChessBoard board = ChessBoard.Restore(
                setup,
                lastMoveFrom,
                lastMoveTo,
                lastMoveType);

            ExerciseProgressStatus status =
                Enum.Parse<ExerciseProgressStatus>(
                    data.Status,
                    ignoreCase: true);

            return UserExerciseProgress.Restore(
                data.UserId,
                exercise,
                board,
                data.AttemptsCount,
                status,
                data.StartedAt,
                data.CompletedAt);
        }
    }
}
