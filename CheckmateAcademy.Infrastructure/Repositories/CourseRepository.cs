using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public Task CreateCourseAsync(Course course, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCourseAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<Course>> GetAllCoursesAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Course?> GetCourseByIdAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCourseAsync(Course course, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
