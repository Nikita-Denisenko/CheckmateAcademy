using CheckmateAcademy.Domain.ValueObjects.Content;

namespace CheckmateAcademy.Domain.Entities
{
    public class ContentSlide : LessonSlide
    {
        public SlideContent Content { get; private set; } = null!;

        public ContentSlide(string title, int order, SlideContent content)
            : base(title, order)
        {
            ArgumentNullException.ThrowIfNull(content);
            Content = content;
        }

        public ContentSlide(
            string title,
            int order,
            DateTime createdAt,
            SlideContent content)
            : base(title, order, createdAt)
        {
            ArgumentNullException.ThrowIfNull(content);
            Content = content;
        }
    }
}
