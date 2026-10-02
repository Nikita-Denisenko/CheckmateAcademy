namespace CheckmateAcademy.Domain.ValueObjects.Content
{
    public sealed record TextBlock : ContentBlock
    {
        public string Text { get; private set; } = string.Empty;

        public TextBlock(string text, int order) : base(order)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be null or whitespace.", nameof(text));

            Text = text;
        }
    }
}
