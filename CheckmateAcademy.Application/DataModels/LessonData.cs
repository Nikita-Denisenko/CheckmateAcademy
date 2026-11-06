namespace CheckmateAcademy.Application.DataModels
{
    public record LessonData
    {
        public int Id { get; init; }
        public int CourseId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public int Order { get; init; }
        public List<LessonSlideData> Slides { get; init; } = [];

        public LessonData(
            int id, 
            int courseId,
            string title, 
            string description, 
            int order, 
            List<LessonSlideData> slides)
        {
            Id = id;
            CourseId = courseId;
            Title = title;
            Description = description;
            Order = order;
            Slides = slides;
        }
    }
}
