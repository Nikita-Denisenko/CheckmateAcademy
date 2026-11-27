using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class StudyRepository : IStudyRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public StudyRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateStudyAsync(Study study, CancellationToken ct)
        {
            var studies = await GetAllStudiesAsync(ct);
            studies.Add(study);
            await WriteStudiesAsync(studies, ct);
        }

        public async Task DeleteStudyAsync(int id, CancellationToken ct)
        {
            var studies = await GetAllStudiesAsync(ct);

            var removedCount = studies.RemoveAll(s => s.Id == id);

            if (removedCount == 0)
                return;

            await WriteStudiesAsync(studies, ct);
        }

        public async Task<List<Study>> GetAllStudiesAsync(CancellationToken ct)
        {
            EnsureFileExists();

            var json = await File.ReadAllTextAsync(
                _storageOptions.StudiesFilePath,
                ct);

            var studiesData = JsonSerializer.Deserialize<List<StudyData>>(
                json,
                JsonOptions) ?? [];

            return studiesData
                .Select(StudyMapper.ToDomain)
                .ToList();
        }

        public async Task<Study?> GetStudyByIdAsync(int id, CancellationToken ct)
        {
            var studies = await GetAllStudiesAsync(ct);

            return studies.FirstOrDefault(s => s.Id == id);
        }

        public async Task UpdateStudyAsync(Study study, CancellationToken ct)
        {
            var studies = await GetAllStudiesAsync(ct);

            var index = studies.FindIndex(s => s.Id == study.Id);

            if (index == -1)
                return;

            studies[index] = study;

            await WriteStudiesAsync(studies, ct);
        }

        private async Task WriteStudiesAsync(
            IEnumerable<Study> studies,
            CancellationToken ct)
        {
            var studiesData = studies
                .Select(StudyMapper.ToData)
                .ToList();

            var json = JsonSerializer.Serialize(studiesData, JsonOptions);

            await File.WriteAllTextAsync(
                _storageOptions.StudiesFilePath,
                json,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.StudiesFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllStudiesAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
