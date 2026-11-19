using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        public Task CreateLessonAsync(Lesson lesson, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLessonAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<Lesson>> GetAllLessonsAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Lesson?> GetLessonByIdAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLessonAsync(Lesson lesson, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
