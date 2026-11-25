using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Game;


namespace CheckmateAcademy.Infrastructure.Mappers;

public static class ExerciseMapper
{
    public static ExerciseData ToData(Exercise exercise)
    {
        ArgumentNullException.ThrowIfNull(exercise);

        return new ExerciseData
        {
            Id = exercise.Id,
            Title = exercise.Title,
            Description = exercise.Description,

            StartPosition = exercise.StartPosition.Pieces.Select(p => new PieceSetupData
            {
                Type = p.Type.ToString(),
                Color = p.Color.ToString(),
                Row = p.Position.Row,
                Column = p.Position.Column
            }).ToList(),

            Solution = exercise.Solution.Select(m => new ExerciseSolutionMoveData
            {
                FromRow = m.From.Row,
                FromColumn = m.From.Column,
                ToRow = m.To.Row,
                ToColumn = m.To.Column,
                Type = m.Type.ToString()
            }).ToList()
        };
    }

    public static Exercise ToDomain(ExerciseData data)
    {
        ArgumentNullException.ThrowIfNull(data);

        var startPosition = new ChessBoardSetup(
            data.StartPosition.Select(p => new PieceSetup
            {
                Type = Enum.Parse<PieceType>(p.Type),
                Color = Enum.Parse<Color>(p.Color),
                Position = new BoardPosition(p.Row, p.Column)
            }));

        var solution = data.Solution.Select(m => new ExerciseSolutionMove
        {
            From = new BoardPosition(m.FromRow, m.FromColumn),
            To = new BoardPosition(m.ToRow, m.ToColumn),
            Type = Enum.Parse<MoveType>(m.Type)
        });

        return new Exercise(
            data.Id,
            data.Title,
            data.Description,
            startPosition,
            solution);
    }
}
