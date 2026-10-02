using CheckmateAcademy.Domain.Common;

namespace CheckmateAcademy.Domain.Entities
{
    public class StudyChapter
    {
        public string Title { get; private set; }
        public int Order { get; private set; }

        public StudyChapter(
            string title,
            int order)
        {
            DomainValidation.NotEmptyString(
                title,
                nameof(title));

            DomainValidation.PositiveOrder(
                order,
                nameof(order));

            Title = title;
            Order = order;
        }
    }
}