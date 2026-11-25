using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        public Task CreateLessonAsync(Lesson lesson, int courseId, CancellationToken ct);
        public Task<Lesson?> GetLessonByIdAsync(int id, CancellationToken ct);
        public Task<List<Lesson>> GetAllLessonsAsync(CancellationToken ct);
        public Task UpdateLessonAsync(Lesson lesson, int courseId, CancellationToken ct);
        public Task DeleteLessonAsync(int id, int courseId, CancellationToken ct);
        public Task<int> GetNextIdAsync(CancellationToken ct);
    }
}
