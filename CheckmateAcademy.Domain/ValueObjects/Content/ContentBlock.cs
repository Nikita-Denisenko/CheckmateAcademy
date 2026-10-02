namespace CheckmateAcademy.Domain.ValueObjects.Content
{
    public abstract record ContentBlock
    {
        public int Order { get; private set; }

        public ContentBlock(int order)
        {
            if (order < 0)
                throw new ArgumentOutOfRangeException(nameof(order), "Order must be a non-negative integer.");

            Order = order;
        }
    }
}
