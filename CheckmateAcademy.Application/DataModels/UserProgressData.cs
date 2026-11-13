namespace CheckmateAcademy.Application.DataModels;

public class UserTaskProgressData
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public List<PieceSetupData> CurrentBoard { get; set; } = [];
    public int AttemptsCount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
