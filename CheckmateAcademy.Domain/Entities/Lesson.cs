namespace CheckmateAcademy.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

    }
}
