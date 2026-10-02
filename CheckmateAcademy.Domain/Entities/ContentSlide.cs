using CheckmateAcademy.Domain.ValueObjects.Content;

namespace CheckmateAcademy.Domain.Entities
{
    public class ContentSlide : LessonSlide
    {
        public SlideContent Content { get; private set; } = null!;

        public ContentSlide(string title, int order, SlideContent content) : base(title, order)
        {
            Content = content;
        }
    }
}
