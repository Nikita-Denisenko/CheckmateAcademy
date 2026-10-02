using CheckmateAcademy.Domain.Common;

namespace CheckmateAcademy.Domain.Entities
{
    public class Study
    {
        public int Id { get; }
        public int UserId { get; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        private readonly List<StudyChapter> _chapters = [];
        public IReadOnlyCollection<StudyChapter> Chapters => _chapters;

        public Study(
            int id,
            int userId,
            string name,
            string? description,
            IEnumerable<StudyChapter> chapters)
        {
            DomainValidation.PositiveId(id, nameof(id));
            DomainValidation.PositiveId(userId, nameof(userId));
            DomainValidation.NotEmptyString(name, nameof(name));

            var studyChapters = DomainValidation.OrderedCollection(
                chapters,
                nameof(chapters),
                chapter => chapter.Order,
                allowEmpty: false);

            Id = id;
            UserId = userId;
            Name = name;
            Description = description;

            _chapters.AddRange(studyChapters);
        }
    }
}