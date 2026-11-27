using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Domain.Entities.Chess;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class UserTaskProgressMapper
    {
        public static UserTaskProgressData ToData(
            UserTaskProgress progress)
        {
            ArgumentNullException.ThrowIfNull(progress);

            return new UserTaskProgressData
            {
                UserId = progress.UserId,
                TaskId = progress.TaskId,

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

                Status = progress.Status.ToString(),
                AttemptsCount = progress.AttemptsCount,
                CompletedAt = progress.CompletedAt
            };
        }

        public static UserTaskProgress ToDomain(
            UserTaskProgressData data,
            ChessTask task)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(task);

            if (data.TaskId != task.Id)
            {
                throw new ArgumentException(
                    "Task ID does not match progress data.",
                    nameof(task));
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
            var board = new ChessBoard(setup);

            var status = Enum.Parse<TaskProgressStatus>(
                data.Status,
                ignoreCase: true);

            return UserTaskProgress.Restore(
                data.UserId,
                task,
                board,
                data.AttemptsCount,
                status,
                data.CompletedAt);
        }
    }
}
