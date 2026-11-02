namespace CheckmateAcademy.Domain.ValueObjects.Courses
{
    public record CourseProgress
    {
        public IReadOnlyList<LessonProgress> LessonsProgress { get; }
        public int CompletionPercentage => TotalPoints > 0 ? (int)((double)ReceivedPoints / TotalPoints * 100) : 0;
        public bool IsCompleted => LessonsProgress.All(lp => lp.IsCompleted);
        public int TotalPoints => LessonsProgress.Sum(l => l.TotalPoints);
        public int ReceivedPoints => LessonsProgress.Sum(l => l.ReceivedPoints);

        public CourseProgress(IReadOnlyCollection<LessonProgress> lessonsProgress)
        {
            if (lessonsProgress == null || lessonsProgress.Count == 0)
                throw new ArgumentNullException(nameof(lessonsProgress));

            LessonsProgress = lessonsProgress.ToList().AsReadOnly();
        }
    }
}
