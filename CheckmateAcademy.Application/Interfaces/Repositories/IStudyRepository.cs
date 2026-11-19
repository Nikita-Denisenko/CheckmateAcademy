using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Application.Interfaces.Repositories
{
    public interface IStudyRepository
    {
        public Task CreateStudyAsync(Study study, CancellationToken ct);
        public Task<Study?> GetStudyByIdAsync(int id, CancellationToken ct);
        public Task<List<Study>> GetAllStudiesAsync(CancellationToken ct);
        public Task UpdateStudyAsync(Study study, CancellationToken ct);
        public Task DeleteStudyAsync(int id, CancellationToken ct);
    }
}
