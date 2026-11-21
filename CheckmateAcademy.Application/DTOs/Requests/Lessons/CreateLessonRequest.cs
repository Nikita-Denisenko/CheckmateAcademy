using CheckmateAcademy.Application.DTOs.Requests.Lessons.Slides;

namespace CheckmateAcademy.Application.DTOs.Requests.Lessons
{
    public record CreateLessonRequest
    {
        public string Title { get; set; } = string.Empty;
        public List<CreateLessonSlideRequest> Slides { get; set; } = [];
    }
}
