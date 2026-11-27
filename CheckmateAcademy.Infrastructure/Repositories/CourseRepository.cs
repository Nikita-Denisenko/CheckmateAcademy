using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public CourseRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateCourseAsync(
            Course course,
            CancellationToken ct)
        {
            var courses = await GetAllCoursesAsync(ct);

            courses.Add(course);

            await WriteCoursesAsync(courses, ct);
        }

        public async Task DeleteCourseAsync(
            int id,
            CancellationToken ct)
        {
            var courses = await GetAllCoursesAsync(ct);

            var removedCount = courses.RemoveAll(c => c.Id == id);

            if (removedCount == 0)
                return;

            await WriteCoursesAsync(courses, ct);
        }

        public async Task<List<Course>> GetAllCoursesAsync(
            CancellationToken ct)
        {
            EnsureFileExists();

            var json = await File.ReadAllTextAsync(
                _storageOptions.CoursesFilePath,
                ct);

            var coursesData = JsonSerializer.Deserialize<List<CourseData>>(
                json,
                JsonOptions) ?? [];

            return coursesData
                .Select(cd => CourseMapper.ToDomain(cd))
                .ToList();
        }

        public async Task<Course?> GetCourseByIdAsync(
            int id,
            CancellationToken ct)
        {
            var courses = await GetAllCoursesAsync(ct);

            return courses.FirstOrDefault(c => c.Id == id);
        }

        public async Task UpdateCourseAsync(
            Course course,
            CancellationToken ct)
        {
            var courses = await GetAllCoursesAsync(ct);

            var index = courses.FindIndex(c => c.Id == course.Id);

            if (index == -1)
                return;

            courses[index] = course;

            await WriteCoursesAsync(courses, ct);
        }

        private async Task WriteCoursesAsync(
            IEnumerable<Course> courses,
            CancellationToken ct)
        {
            var coursesData = courses
                .Select(CourseMapper.ToData)
                .ToList();

            var json = JsonSerializer.Serialize(coursesData, JsonOptions);

            await File.WriteAllTextAsync(
                _storageOptions.CoursesFilePath,
                json,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.CoursesFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        public async Task<int> GetNextIdAsync(CancellationToken ct)
        {
            var entities = await GetAllCoursesAsync(ct);
            return entities.Count == 0 ? 1 : checked(entities.Max(x => x.Id) + 1);
        }
    }
}
