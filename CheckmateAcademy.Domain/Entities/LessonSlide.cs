using CheckmateAcademy.Domain.Common;

namespace CheckmateAcademy.Domain.Entities
{
    public abstract class LessonSlide
    {
        public string Title { get; private set; } = string.Empty;
        public int Order { get; private set; } = 1;
        public DateTime CreatedAt { get; private set; }

        protected LessonSlide(string title, int order)
        {
            DomainValidation.NotEmptyString(
                title,
                nameof(title));

            DomainValidation.PositiveOrder(
                order,
                nameof(order));

            Title = title;
            Order = order;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
