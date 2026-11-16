using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class UserCourseMapper
    {
        public static CourseProgressData MapToDataModel(UserCourse userCourse)
        {
            ArgumentNullException.ThrowIfNull(userCourse);

            return CourseProgressMapper.ToData(
                userCourse.Progress,
                userCourse.UserId,
                userCourse.CourseId
            );
        }

        public static UserCourse MapToDomainEntity(CourseProgressData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return new UserCourse(
                data.UserId,
                data.CourseId,
                CourseProgressMapper.ToDomain(data)
            );
        }
    }
}
