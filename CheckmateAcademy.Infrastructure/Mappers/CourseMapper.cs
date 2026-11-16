using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class CourseMapper
    {
        public static CourseData ToData(Course course)
        {
            ArgumentNullException.ThrowIfNull(course);

            return new CourseData(
                course.Id,
                course.Name,
                course.Description
            );
        }

        public static Course ToDomain(
            CourseData data,
            IEnumerable<Lesson>? lessons = null)
        {
            ArgumentNullException.ThrowIfNull(data);

            return new Course(
                data.Id,
                data.Name,
                data.Description,
                lessons
            );
        }
    }
}
