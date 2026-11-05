namespace CheckmateAcademy.Application.DTOs.Requests.Lessons
{
    public record GetLessonsRequest
    {
        public int UserId { get; init; }
        public int Page { get; init; } = 1;
        public int Size { get; init; } = 10;
        
        public GetLessonsRequest(int userId, int page = 1, int size = 10)
        {
            if (userId <= 0)
                throw new ArgumentException("UserId must be greater than zero.", nameof(userId));

            if (page <= 0)
                throw new ArgumentException("Page must be greater than zero.", nameof(page));

            if (size <= 0)
                throw new ArgumentException("Size must be greater than zero.", nameof(size));

            UserId = userId;
            Page = page;
            Size = size;
        }
    }
}
