namespace CheckmateAcademy.Application.DataModels;

public record TaskSolutionMoveData
{
    public int FromRow { get; set; }
    public int FromColumn { get; set; }
    public int ToRow { get; set; }
    public int ToColumn { get; set; }
    public string Type { get; set; } = string.Empty;
}
