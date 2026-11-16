namespace CheckmateAcademy.Application.DataModels;

public record ChessTaskData
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<PieceSetupData> StartPosition { get; init; } = [];
    public List<TaskSolutionMoveData> Solution { get; init; } = [];
    public int Difficulty { get; init; }
}
