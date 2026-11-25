using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public LessonRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateLessonAsync(Lesson lesson, int courseId, CancellationToken ct)
        {
            EnsureFileExists();

            var lessonData = LessonMapper.ToData(lesson, courseId);
            var json = JsonSerializer.Serialize(lessonData, JsonOptions);

            await File.AppendAllTextAsync(
                _storageOptions.LessonsFilePath,
                json + Environment.NewLine,
                ct);
        }

        public async Task DeleteLessonAsync(int id, int courseId, CancellationToken ct)
        {
            var lessons = await GetAllLessonsAsync(ct);

            var lesson = lessons.FirstOrDefault(l => l.Id == id)
                ?? throw new InvalidOperationException("Lesson not found");

            lessons.Remove(lesson);

            await WriteLessonsAsync(lessons, courseId, ct);
        }

        public async Task<List<Lesson>> GetAllLessonsAsync(CancellationToken ct)
        {
            EnsureFileExists();

            var lines = await File.ReadAllLinesAsync(
                _storageOptions.LessonsFilePath,
                ct);

            var lessons = new List<Lesson>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var lessonData = JsonSerializer.Deserialize<LessonData>(
                    line,
                    JsonOptions);

                if (lessonData is null)
                    continue;

                lessons.Add(LessonMapper.ToDomain(lessonData));
            }

            return lessons;
        }

        public async Task<Lesson?> GetLessonByIdAsync(int id, CancellationToken ct)
        {
            var lessons = await GetAllLessonsAsync(ct);

            return lessons.FirstOrDefault(l => l.Id == id);
        }

        public async Task UpdateLessonAsync(Lesson lesson, int courseId, CancellationToken ct)
        {
            var lessons = await GetAllLessonsAsync(ct);

            var index = lessons.FindIndex(l => l.Id == lesson.Id);

            if (index == -1)
                throw new InvalidOperationException("Lesson not found");

            lessons[index] = lesson;

            await WriteLessonsAsync(lessons, courseId, ct);
        }

        private async Task WriteLessonsAsync(
            IEnumerable<Lesson> lessons,
            int courseId,
            CancellationToken ct)
        {
            var lines = lessons.Select(lesson =>
                JsonSerializer.Serialize(
                    LessonMapper.ToData(lesson, courseId),
                    JsonOptions));

            await File.WriteAllLinesAsync(
                _storageOptions.LessonsFilePath,
                lines,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.LessonsFilePath;
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
            var entities = await GetAllLessonsAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
