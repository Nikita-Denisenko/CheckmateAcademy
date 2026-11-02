namespace CheckmateAcademy.Domain.ValueObjects.Courses
{
    public record CourseProgress
    {
        public int CompletedSlides { get; }
        public int TotalSlides { get; }
        public IReadOnlyList<LessonProgress> LessonsProgress { get; }
        public int CompletionPercentage =>
            TotalSlides == 0
            ? 0
            : (int)((double)CompletedSlides / TotalSlides * 100);
        public bool IsCompleted => CompletedSlides >= TotalSlides;
        public int TotalPoints => LessonsProgress.Sum(l => l.TotalPoints);
        public int ReceivedPoints => LessonsProgress.Sum(l => l.ReceivedPoints);

        public CourseProgress(int totalSlides,
            IReadOnlyCollection<LessonProgress> lessonsProgress,
            int completedSlides = 0)
        {
            if (completedSlides < 0)
                throw new ArgumentException("Completed slides cannot be negative.", nameof(completedSlides));

            if (totalSlides <= 0)
                throw new ArgumentException("Total slides must be greater than zero.", nameof(totalSlides));

            if (completedSlides > totalSlides)
                throw new ArgumentException("Completed slides cannot exceed total slides.", nameof(completedSlides));

            if (lessonsProgress == null || lessonsProgress.Count == 0)
                throw new ArgumentNullException(nameof(lessonsProgress));

            CompletedSlides = completedSlides;
            TotalSlides = totalSlides;
            LessonsProgress = lessonsProgress.ToList().AsReadOnly();
        }
    }
}
