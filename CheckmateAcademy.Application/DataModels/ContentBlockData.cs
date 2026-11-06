namespace CheckmateAcademy.Application.DataModels
{
    public record ContentBlockData
    {
        public int Order { get; init; }
        public string? ImageUrl { get; init; }
        public string? Text { get; init; }

        public ContentBlockData(int order, string? imageUrl, string? text)
        {
            Order = order;
            ImageUrl = imageUrl;
            Text = text;
        }
    }
}
