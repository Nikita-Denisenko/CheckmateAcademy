using CheckmateAcademy.Domain.Common;

namespace CheckmateAcademy.Domain.Entities
{
    public class Course
    {
        public int Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private readonly List<Lesson> _lessons = [];
        public IReadOnlyCollection<Lesson> Lessons => _lessons;

        public Course(
            int id,
            string name,
            string description,
            IEnumerable<Lesson>? lessons = null)
        {
            DomainValidation.PositiveId(id, nameof(id));
            DomainValidation.NotEmptyString(name, nameof(name));
            DomainValidation.NotEmptyString(description, nameof(description));

            var courseLessons = DomainValidation.OrderedCollection(
                lessons,
                nameof(lessons),
                lesson => lesson.Order);

            Id = id;
            Name = name;
            Description = description;

            _lessons.AddRange(courseLessons);
        }
    }
}
