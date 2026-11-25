using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class ChessTaskMapper
    {
        public static ChessTaskData ToData(ChessTask task)
        {
            return new ChessTaskData
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                StartPosition = task.StartPosition.Pieces.Select(p => new PieceSetupData
                {
                    Type = p.Type.ToString(),
                    Color = p.Color.ToString(),
                    Row = p.Position.Row,
                    Column = p.Position.Column
                }).ToList(),
                Solution = task.Solution.Select(m => new TaskSolutionMoveData
                {
                    FromRow = m.From.Row,
                    FromColumn = m.From.Column,
                    ToRow = m.To.Row,
                    ToColumn = m.To.Column
                }).ToList(),
                Difficulty = task.Difficulty
            };
        }

        public static ChessTask ToDomain(ChessTaskData data)
        {
            var startPosition = new ChessBoardSetup(data.StartPosition.Select(p => new PieceSetup
            {
                Type = Enum.Parse<PieceType>(p.Type),
                Color = Enum.Parse<Color>(p.Color),
                Position = new BoardPosition(p.Row, p.Column)
            }).ToList());
            var solution = data.Solution.Select(m => new ExerciseSolutionMove
            {
                From = new BoardPosition(m.FromRow, m.FromColumn),
                To = new BoardPosition(m.ToRow, m.ToColumn)
            }).ToList();
            return new ChessTask(data.Id, data.Title, data.Description, startPosition, solution, data.Difficulty);
        }
    }
}

