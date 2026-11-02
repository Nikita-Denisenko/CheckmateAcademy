using CheckmateAcademy.Domain.ValueObjects.Courses;

namespace CheckmateAcademy.Domain.Entities
{
    public class UserCourse
    {
        public int UserId { get; }
        public int CourseId { get; }
        public CourseProgress Progress { get; private set; } = null!;

        public UserCourse(int userId, int courseId, CourseProgress progress)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));
            if (courseId <= 0)
                throw new ArgumentException("Course ID must be greater than zero.", nameof(courseId));
            if (progress is null)
                throw new ArgumentNullException(nameof(progress));

            UserId = userId;
            CourseId = courseId;
            Progress = progress;
        }
    }
}
