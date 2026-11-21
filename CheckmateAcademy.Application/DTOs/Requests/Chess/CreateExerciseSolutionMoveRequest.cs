namespace CheckmateAcademy.Application.DTOs.Requests.Chess
{
    public record CreateExerciseSolutionMoveRequest
    {
        public int FromRow { get; set; }
        public int FromColumn { get; set; }
        public int ToRow { get; set; }
        public int ToColumn { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
