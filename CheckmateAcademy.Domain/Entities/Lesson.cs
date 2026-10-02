using CheckmateAcademy.Domain.Common;

namespace CheckmateAcademy.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Order { get; }

        private readonly List<LessonSlide> _slides = [];
        public IReadOnlyCollection<LessonSlide> Slides => _slides;

        public Lesson(
            int id,
            string title,
            string description,
            int order,
            IEnumerable<LessonSlide>? slides = null)
        {
            DomainValidation.PositiveId(id, nameof(id));
            DomainValidation.NotEmptyString(title, nameof(title));

            DomainValidation.PositiveOrder(
                order,
                nameof(order));

            DomainValidation.NotEmptyString(
                description,
                nameof(description));

            var lessonSlides = DomainValidation.OrderedCollection(
                slides,
                nameof(slides),
                slide => slide.Order);

            Id = id;
            Title = title;
            Description = description;
            Order = order;

            _slides.AddRange(lessonSlides);
        }
    }
}
