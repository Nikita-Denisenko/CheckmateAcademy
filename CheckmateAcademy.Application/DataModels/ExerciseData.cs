namespace CheckmateAcademy.Application.DataModels;

public record ExerciseData
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<PieceSetupData> StartPosition { get; set; } = [];
    public List<ExerciseSolutionMoveData> Solution { get; set; } = [];
}
