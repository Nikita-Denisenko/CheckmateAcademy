using CheckmateAcademy.Domain.Common;

namespace CheckmateAcademy.Domain.Entities
{
    public class Follow
    {
        public int Id { get; }
        public int FollowerId { get; }
        public int TargetId { get; }
        public DateTime CreatedAt { get; }

        public Follow(
            int id,
            int followerId,
            int targetId)
        {
            DomainValidation.PositiveId(id, nameof(id));
            DomainValidation.PositiveId(followerId, nameof(followerId));
            DomainValidation.PositiveId(targetId, nameof(targetId));

            if (followerId == targetId)
                throw new ArgumentException(
                    "User cannot follow themselves.",
                    nameof(targetId));

            Id = id;
            FollowerId = followerId;
            TargetId = targetId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}