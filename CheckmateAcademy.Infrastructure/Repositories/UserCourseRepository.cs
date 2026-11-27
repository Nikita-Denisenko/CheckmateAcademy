using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Infrastructure.Configuration;
using CheckmateAcademy.Infrastructure.Mappers;
using System.Text.Json;

namespace CheckmateAcademy.Infrastructure.Repositories
{
    public class UserCourseRepository : IUserCourseRepository
    {
        private readonly StorageOptions _storageOptions;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public UserCourseRepository(StorageOptions storageOptions)
        {
            _storageOptions = storageOptions
                ?? throw new ArgumentNullException(nameof(storageOptions));
        }

        public async Task CreateUserCourseAsync(
            UserCourse userCourse,
            CancellationToken ct)
        {
            var userCourses = await GetAllUserCoursesAsync(ct);

            var alreadyExists = userCourses.Any(uc =>
                uc.UserId == userCourse.UserId &&
                uc.CourseId == userCourse.CourseId);

            if (alreadyExists)
                throw new InvalidOperationException(
                    "User is already enrolled in this course.");

            userCourses.Add(userCourse);

            await WriteUserCoursesAsync(userCourses, ct);
        }

        public async Task<UserCourse?> GetUserCourseAsync(
            int userId,
            int courseId,
            CancellationToken ct)
        {
            var userCourses = await GetAllUserCoursesAsync(ct);

            return userCourses.FirstOrDefault(uc =>
                uc.UserId == userId &&
                uc.CourseId == courseId);
        }

        public async Task<List<UserCourse>> GetAllUserCoursesAsync(
            CancellationToken ct)
        {
            EnsureFileExists();

            var json = await File.ReadAllTextAsync(
                _storageOptions.UserCoursesFilePath,
                ct);

            var userCoursesData = JsonSerializer.Deserialize<List<CourseProgressData>>(
                json,
                JsonOptions) ?? [];

            return userCoursesData
                .Select(UserCourseMapper.ToDomain)
                .ToList();
        }

        public async Task UpdateUserCourseAsync(
            UserCourse userCourse,
            CancellationToken ct)
        {
            var userCourses = await GetAllUserCoursesAsync(ct);

            var index = userCourses.FindIndex(uc =>
                uc.UserId == userCourse.UserId &&
                uc.CourseId == userCourse.CourseId);

            if (index == -1)
                return;

            userCourses[index] = userCourse;

            await WriteUserCoursesAsync(userCourses, ct);
        }

        public async Task DeleteUserCourseAsync(
            int userId,
            int courseId,
            CancellationToken ct)
        {
            var userCourses = await GetAllUserCoursesAsync(ct);

            var removedCount = userCourses.RemoveAll(uc =>
                uc.UserId == userId &&
                uc.CourseId == courseId);

            if (removedCount == 0)
                return;

            await WriteUserCoursesAsync(userCourses, ct);
        }

        private async Task WriteUserCoursesAsync(
            IEnumerable<UserCourse> userCourses,
            CancellationToken ct)
        {
            var userCoursesData = userCourses
                .Select(UserCourseMapper.ToData)
                .ToList();

            var json = JsonSerializer.Serialize(userCoursesData, JsonOptions);

            await File.WriteAllTextAsync(
                _storageOptions.UserCoursesFilePath,
                json,
                ct);
        }

        private void EnsureFileExists()
        {
            var filePath = _storageOptions.UserCoursesFilePath;
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");
        }
    }
}
