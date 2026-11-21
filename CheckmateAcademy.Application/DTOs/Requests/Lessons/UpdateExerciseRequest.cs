using CheckmateAcademy.Application.DTOs.Requests.Chess;

namespace CheckmateAcademy.Application.DTOs.Requests.Lessons
{
    public record UpdateExerciseRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CreateChessBoardSetupRequest StartPosition { get; set; } = null!;
        public List<CreateExerciseSolutionMoveRequest> Solution { get; set; } = [];
    }
}