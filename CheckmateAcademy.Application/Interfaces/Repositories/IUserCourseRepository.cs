using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IUserCourseRepository
    {
        public Task CreateUserCourseAsync(UserCourse userCourse, CancellationToken ct);
        public Task<UserCourse?> GetUserCourseAsync(int userId, int courseId, CancellationToken ct);
        public Task<List<UserCourse>> GetAllUserCoursesAsync(CancellationToken ct);
        public Task UpdateUserCourseAsync(UserCourse userCourse, CancellationToken ct);
        public Task DeleteUserCourseAsync(int userId, int courseId, CancellationToken ct);
    }
}
