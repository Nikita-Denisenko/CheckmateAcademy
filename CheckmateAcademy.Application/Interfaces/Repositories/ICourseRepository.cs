using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        public Task CreateCourseAsync(Course course, CancellationToken ct);
        public Task<Course?> GetCourseByIdAsync(int id, CancellationToken ct);
        public Task<List<Course>> GetAllCoursesAsync(CancellationToken ct);
        public Task UpdateCourseAsync(Course course, CancellationToken ct);
        public Task DeleteCourseAsync(int id, CancellationToken ct);
    }
}
