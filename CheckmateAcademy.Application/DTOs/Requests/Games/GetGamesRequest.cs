namespace CheckmateAcademy.Application.DTOs.Requests.Games
{
    public record GetGamesRequest
    {
        public int UserId { get; init; }
        public int Page { get; init; } = 1;
        public int Size { get; init; } = 10;
    }
}
