namespace CheckmateAcademy.Application.DataModels
{
    public class UserTaskProgressData
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public List<PieceSetupData> CurrentBoard { get; set; } = [];
        public string Status { get; set; } = string.Empty;
        public int AttemptsCount { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
