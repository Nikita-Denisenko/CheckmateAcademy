namespace CheckmateAcademy.Application.DTOs.Responses.Lessons.Slides.ContentSlide
{
    public record ContentSlideResponse : SlideResponse
    {
        public IReadOnlyList<ContentBlockResponse> Blocks { get; init; } = [];
        public ContentSlideResponse(
            string title, 
            int order,
            DateTime createdAt, 
            IReadOnlyList<ContentBlockResponse> blocks)
            : base(title, order, createdAt)
        {
            Blocks = blocks;
        }
    }
}
