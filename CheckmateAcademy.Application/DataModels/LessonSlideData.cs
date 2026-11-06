namespace CheckmateAcademy.Application.DataModels
{
    public record LessonSlideData
    {
        public string Title { get; } = string.Empty;
        public int Order { get; } = 1;
        public DateTime CreatedAt { get; }
        public SlideContentData? SlideContentData { get;  }
        public ExerciseData? ExerciseData { get; }

        public LessonSlideData(
            string title, 
            int order, 
            DateTime createdAt, 
            SlideContentData? slideContentData, 
            ExerciseData? exerciseData)
        {
            Title = title;
            Order = order;
            CreatedAt = createdAt;
            SlideContentData = slideContentData;
            ExerciseData = exerciseData;
        }
    }
}
