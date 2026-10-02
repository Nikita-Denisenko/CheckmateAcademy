using CheckmateAcademy.Domain.ValueObjects.Content;

namespace CheckmateAcademy.Domain.Entities
{
    public class ContentSlide : LessonSlide
    {
        public SlideContent Content { get; private set; } = new SlideContent();
    }
}
