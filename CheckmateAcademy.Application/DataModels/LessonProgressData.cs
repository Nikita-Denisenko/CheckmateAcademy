namespace CheckmateAcademy.Application.DataModels
{
    public record LessonProgressData
    {
        public int LessonId { get; }
        public int ReceivedPoints { get; }
        public int TotalPoints { get; }
        public int CompletedSlides { get; }
        public int TotalSlides { get; }

        public LessonProgressData(
            int lessonId, 
            int receivedPoints, 
            int totalPoints, 
            int completedSlides, 
            int totalSlides)
        {
            LessonId = lessonId;
            ReceivedPoints = receivedPoints;
            TotalPoints = totalPoints;
            CompletedSlides = completedSlides;
            TotalSlides = totalSlides;
        }
    }
}
