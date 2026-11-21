namespace CheckmateAcademy.Application.DTOs.Requests.Lessons
{
    public record UpdateExerciseSlideRequest
    {
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public UpdateExerciseRequest Exercise { get; set; } = null!;
    }
}
