namespace CheckmateAcademy.Application.DataModels
{
    public record FollowData
    {
        public int Id { get; init; }
        public int FollowerId { get; init; }
        public int TargetId { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
