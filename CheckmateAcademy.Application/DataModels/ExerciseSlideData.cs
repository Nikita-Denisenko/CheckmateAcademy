namespace CheckmateAcademy.Application.DataModels;

public class ExerciseSlideData
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public int ExerciseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
}
