namespace CheckmateAcademy.Application.DTOs.Responses.Lessons.Slides.ContentSlide
{
    public record ContentBlockResponse
    {
        public int Order { get; init; }
        public string? Text { get; init; }
        public string? ImageUrl { get; init; }

        public ContentBlockResponse(int order, string? text, string? imageUrl)
        {
            if (order < 0)
            {
                throw new ArgumentException(
                    "Order must be a non-negative integer.",
                    nameof(order));
            }

            if (string.IsNullOrWhiteSpace(text) && string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentException(
                    "Either Text or ImageUrl must be provided.",
                    nameof(text));
            }

            if (!string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentException(
                    "Only one of Text or ImageUrl can be provided.",
                    nameof(text));
            }

            if (!string.IsNullOrWhiteSpace(imageUrl) && !Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                throw new ArgumentException(
                    "ImageUrl must be a valid absolute URL.",
                    nameof(imageUrl));
            }

            Order = order;
            Text = text;
            ImageUrl = imageUrl;
        }
    }
}
