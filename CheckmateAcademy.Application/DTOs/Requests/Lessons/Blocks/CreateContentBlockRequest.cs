namespace CheckmateAcademy.Application.DTOs.Requests.Lessons.Blocks
{
    public record CreateContentBlockRequest
    {
        public string Type { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? Text { get; set; }
        public string? ImageUrl { get; set; }
    }
}
