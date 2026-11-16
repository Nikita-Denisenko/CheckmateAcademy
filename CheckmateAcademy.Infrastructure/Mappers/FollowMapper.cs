using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class FollowMapper
    {
        public static FollowData ToData(Follow follow)
        {
            return new FollowData
            {
                Id = follow.Id,
                FollowerId = follow.FollowerId,
                TargetId = follow.TargetId,
                CreatedAt = follow.CreatedAt
            };
        }

        public static Follow ToDomain(FollowData followData)
        {
            return new Follow(
                followData.Id,
                followData.FollowerId,
                followData.TargetId,
                followData.CreatedAt);
        }
    }
}
