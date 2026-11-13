using CheckmateAcademy.Domain.ValueObjects.Game;

namespace CheckmateAcademy.Domain.Entities
{
    public class StudyMoveNode
    {
        public int Id { get; private set; }
        public StudyMove Move { get; private set; }
        private readonly List<StudyMoveNode> _nextMoves = [];
        public IReadOnlyCollection<StudyMoveNode> NextMoves => _nextMoves.AsReadOnly();

        protected StudyMoveNode()
        {
            Move = null!;
        }

        public StudyMoveNode(
            int id,
            StudyMove move,
            IEnumerable<StudyMoveNode>? nextMoves = null)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            ArgumentNullException.ThrowIfNull(move);

            Id = id;
            Move = move;

            if (nextMoves != null)
            {
                var nodes = nextMoves.ToList();

                if (nodes.Any(x => x.Id == id))
                    throw new ArgumentException(
                        "A node cannot contain itself as a continuation.");

                if (nodes.GroupBy(x => x.Id).Any(g => g.Count() > 1))
                    throw new ArgumentException(
                        "Duplicate continuations are not allowed.");

                _nextMoves.AddRange(nodes);
            }
        }

        public void AddContinuation(StudyMoveNode node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (node.Id == Id)
                throw new InvalidOperationException(
                    "A node cannot contain itself as a continuation.");

            if (_nextMoves.Any(x => x.Id == node.Id))
                throw new InvalidOperationException(
                    "This continuation already exists.");

            _nextMoves.Add(node);
        }

        public void RemoveContinuation(int nodeId)
        {
            var node = _nextMoves.FirstOrDefault(x => x.Id == nodeId);

            if (node == null)
                throw new InvalidOperationException(
                    "Continuation not found.");

            _nextMoves.Remove(node);
        }
    }
}
