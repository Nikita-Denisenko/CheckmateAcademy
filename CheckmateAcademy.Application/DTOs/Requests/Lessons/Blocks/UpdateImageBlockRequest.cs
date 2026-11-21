namespace CheckmateAcademy.Application.DTOs.Requests.Lessons.Blocks
{
    public record UpdateImageBlockRequest : UpdateContentBlockRequest
    {
        public string ImageUrl { get; set; } = string.Empty;
    }
}
