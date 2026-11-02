namespace CheckmateAcademy.Domain.ValueObjects.Courses
{
    public record LessonProgress
    {
        public int LessonId { get; }
        public int ReceivedPoints { get; }
        public int TotalPoints { get; }
        public int CompletedSlides { get; }
        public int TotalSlides { get; }
        public bool IsCompleted => CompletedSlides == TotalSlides;

        public LessonProgress(
            int lessonId, 
            int receivedPoints, 
            int totalPoints, 
            int totalSlides,
            int completedSlides = 0)
        {
            if (lessonId <= 0)
                throw new ArgumentException("Lesson ID must be greater than zero.", nameof(lessonId));

            if (receivedPoints < 0)
                throw new ArgumentException("Received points cannot be negative.", nameof(receivedPoints));

            if (totalPoints < 0)
                throw new ArgumentException("Total points must be greater than zero.", nameof(totalPoints));

            if (receivedPoints > totalPoints)
                throw new ArgumentException("Received points cannot exceed total points.", nameof(receivedPoints));

            if (completedSlides < 0)
                throw new ArgumentException("Completed slides cannot be negative.", nameof(completedSlides));

            if (totalSlides <= 0)
                throw new ArgumentException("Total slides must be greater than zero.", nameof(totalSlides));

            if (completedSlides > totalSlides)
                throw new ArgumentException("Completed slides cannot exceed total slides.", nameof(completedSlides));

            LessonId = lessonId;
            ReceivedPoints = receivedPoints;
            TotalPoints = totalPoints;
            CompletedSlides = completedSlides;
            TotalSlides = totalSlides;
        }
    }
}
