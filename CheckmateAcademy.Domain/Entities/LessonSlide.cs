namespace CheckmateAcademy.Domain.Entities
{
    public abstract class LessonSlide
    {
        public int Id { get; }
        public string Title { get; private set; } = string.Empty;
        public int Order { get; private set; }
        public DateTime Created { get; private set; }
    }
}
