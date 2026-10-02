namespace CheckmateAcademy.Domain.ValueObjects.Content
{
    public record SlideContent
    {
        private readonly List<ContentBlock> _blocks = new();
        public IReadOnlyCollection<ContentBlock> Blocks => _blocks;

        public SlideContent(IEnumerable<ContentBlock> blocks)
        {
            var blocksList = blocks?.ToList() 
                ?? throw new ArgumentNullException(nameof(blocks), "Blocks collection cannot be null.");

            if (blocksList.Count == 0)
                throw new ArgumentException("Blocks collection cannot be null or empty.", nameof(blocks));

            if (blocksList[0].Order != 1)
                throw new ArgumentException(
                    "The first block must have order 1.",
                    nameof(blocks));

            if (blocksList
                .Zip(blocksList
                .Skip(1))
                .Any(pair => pair.First.Order + 1 != pair.Second.Order))
                throw new ArgumentException("Blocks must be in order.", nameof(blocks));

            _blocks.AddRange(blocksList);
        }
    }
}
