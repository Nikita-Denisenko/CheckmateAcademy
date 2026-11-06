namespace CheckmateAcademy.Application.DataModels
{
    public record CourseProgressData
    {
        public int UserId { get; }
        public int CourseId { get; }
        public List<LessonProgressData> LessonProgress { get; init; } = [];

        public CourseProgressData(
            int userId,
            int courseId,
            List<LessonProgressData> lessonProgress)
        {
            UserId = userId;
            CourseId = courseId;
            LessonProgress = lessonProgress;
        }
    }
}
