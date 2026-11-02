namespace CheckmateAcademy.Domain.ValueObjects.Courses
{
    public record LessonProgress
    {
        public int LessonId { get; }
        public int ReceivedPoints { get; }
        public int TotalPoints { get; }

        public LessonProgress(int lessonId, int receivedPoints, int totalPoints)
        {
            if (lessonId <= 0)
                throw new ArgumentException("Lesson ID must be greater than zero.", nameof(lessonId));

            if (receivedPoints < 0)
                throw new ArgumentException("Received points cannot be negative.", nameof(receivedPoints));

            if (totalPoints < 0)
                throw new ArgumentException("Total points must be greater than zero.", nameof(totalPoints));

            if (receivedPoints > totalPoints)
                throw new ArgumentException("Received points cannot exceed total points.", nameof(receivedPoints));

            LessonId = lessonId;
            ReceivedPoints = receivedPoints;
            TotalPoints = totalPoints;
        }
    }
}
