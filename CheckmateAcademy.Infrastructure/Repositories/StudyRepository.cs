using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class StudyRepository : IStudyRepository
    {
        public Task CreateStudyAsync(Study study, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudyAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<List<Study>> GetAllStudiesAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Study?> GetStudyByIdAsync(int id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudyAsync(Study study, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
