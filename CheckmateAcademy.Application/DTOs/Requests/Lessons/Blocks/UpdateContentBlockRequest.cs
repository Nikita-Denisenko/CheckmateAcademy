namespace CheckmateAcademy.Application.DTOs.Requests.Lessons.Blocks
{
    public abstract record UpdateContentBlockRequest
    {
        public int Order { get; set; }
    }
}
