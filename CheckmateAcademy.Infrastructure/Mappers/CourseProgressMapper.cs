using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.ValueObjects.Courses;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class CourseProgressMapper
    {
        public static CourseProgressData ToData(
            CourseProgress progress,
            int userId,
            int courseId)
        {
            ArgumentNullException.ThrowIfNull(progress);

            return new CourseProgressData(
                userId,
                courseId,
                progress.LessonsProgress
                    .Select(LessonProgressMapper.ToData)
                    .ToList()
            );
        }

        public static CourseProgress ToDomain(CourseProgressData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return new CourseProgress(
                data.LessonProgress
                    .Select(LessonProgressMapper.ToDomain)
                    .ToList()
            );
        }
    }
}
