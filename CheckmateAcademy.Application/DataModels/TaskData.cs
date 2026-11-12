namespace CheckmateAcademy.Data.DataModels;

public record TaskData
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<PieceSetupData> StartPosition { get; set; } = [];
    public List<TaskSolutionMoveData> Solution { get; set; } = [];
    public int Difficulty { get; set; }
}
