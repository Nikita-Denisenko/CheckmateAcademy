namespace CheckmateAcademy.Data.DataModels;

public class UserExerciseProgressData
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ExerciseId { get; set; }
    public List<PieceSetupData> CurrentBoard { get; set; } = [];
    public int AttemptsCount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
