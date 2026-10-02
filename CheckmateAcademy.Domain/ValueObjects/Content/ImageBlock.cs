namespace CheckmateAcademy.Domain.ValueObjects.Content
{
    public sealed record ImageBlock : ContentBlock
    {
        public string ImageUrl { get; } = string.Empty;

        public ImageBlock(string imageUrl, int order) : base(order)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ArgumentException("Image URL cannot be null or whitespace.", nameof(imageUrl));

            ImageUrl = imageUrl;
        }
    }
}
