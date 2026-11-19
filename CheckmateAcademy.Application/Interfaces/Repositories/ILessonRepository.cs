using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        public Task CreateLessonAsync(Lesson lesson, CancellationToken ct);
        public Task<Lesson?> GetLessonByIdAsync(int id, CancellationToken ct);
        public Task<List<Lesson>> GetAllLessonsAsync(CancellationToken ct);
        public Task UpdateLessonAsync(Lesson lesson, CancellationToken ct);
        public Task DeleteLessonAsync(int id, CancellationToken ct);
    }
}
