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
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public StudyRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateStudyAsync(Study study, CancellationToken ct)
        {
            EnsureFileExists();

            var studyData = StudyMapper.ToData(study);
            var json = JsonSerializer.Serialize(studyData, JsonOptions);

            await File.AppendAllTextAsync(
                _storageOptions.StudiesFilePath,
                json + Environment.NewLine,
                ct);
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

            var lines = await File.ReadAllLinesAsync(
                _storageOptions.StudiesFilePath,
                ct);

            var studies = new List<Study>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var studyData = JsonSerializer.Deserialize<StudyData>(
                    line,
                    JsonOptions);

                if (studyData is null)
                    continue;

                studies.Add(StudyMapper.ToDomain(studyData));
            }

            return studies;
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
            var lines = studies.Select(study =>
                JsonSerializer.Serialize(
                    StudyMapper.ToData(study),
                    JsonOptions));

            await File.WriteAllLinesAsync(
                _storageOptions.StudiesFilePath,
                lines,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.StudiesFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
            {
                using var stream = File.Create(filePath);
            }
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllStudiesAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
