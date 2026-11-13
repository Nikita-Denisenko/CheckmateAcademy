using CheckmateAcademy.Domain.Common;
using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities
{
    public class StudyChapter
    {
        public int Id { get; }
        public string Title { get; private set; }
        public int Order { get; private set; }
        public ChessBoardSetup InitialPosition { get; }
        private readonly List<StudyMoveNode> _moves = [];
        public IReadOnlyCollection<StudyMoveNode> Moves => _moves;

        public StudyChapter(
            int id,
            string title,
            int order,
            ChessBoardSetup initialPosition,
            IEnumerable<StudyMoveNode>? moves = null)
        {
            DomainValidation.PositiveId(id, nameof(id));
            DomainValidation.NotEmptyString(title, nameof(title));
            DomainValidation.PositiveOrder(order, nameof(order));

            Id = id;
            Title = title;
            Order = order;
            InitialPosition = initialPosition
                ?? throw new ArgumentNullException(nameof(initialPosition));

            if (moves != null)
                _moves.AddRange(moves);
        }
    }
}
