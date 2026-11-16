using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class StudyMoveMapper
    {
        public static StudyMove ToDomain(StudyMoveData data)
        {
            return new StudyMove(
                new BoardPosition(data.FromRow, data.FromColumn),
                new BoardPosition(data.ToRow, data.ToColumn),
                Enum.Parse<MoveType>(data.Type, true),
                data.Notation,
                data.Comment);
        }

        public static StudyMoveData ToData(StudyMove move)
        {
            return new StudyMoveData
            {
                FromRow = move.From.Row,
                FromColumn = move.From.Column,
                ToRow = move.To.Row,
                ToColumn = move.To.Column,
                Type = move.Type.ToString(),
                Notation = move.Notation,
                Comment = move.Comment
            };
        }
    }
}
