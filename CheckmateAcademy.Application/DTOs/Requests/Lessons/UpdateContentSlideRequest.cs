using CheckmateAcademy.Application.DTOs.Requests.Lessons.Blocks;

namespace CheckmateAcademy.Application.DTOs.Requests.Lessons
{
    public record UpdateContentSlideRequest
    {
        public string Title { get; set; } = string.Empty;
        public int Order { get; set; }
        public List<UpdateContentBlockRequest> Blocks { get; set; } = [];
    }
}
