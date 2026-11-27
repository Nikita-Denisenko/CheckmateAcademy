namespace CheckmateAcademy.Infrastructure.Configuration
{
    public class StorageOptions
    {
        public string DataDirectory { get; set; } = ResolveDataDirectory();

        public string BotsFilePath => Path.Combine(DataDirectory, "bots.json");
        public string CoursesFilePath => Path.Combine(DataDirectory, "courses.json");
        public string ExercisesFilePath => Path.Combine(DataDirectory, "exercises.json");
        public string FollowsFilePath => Path.Combine(DataDirectory, "follows.json");
        public string GamesFilePath => Path.Combine(DataDirectory, "games.json");
        public string LessonsFilePath => Path.Combine(DataDirectory, "lessons.json");
        public string NotificationsFilePath => Path.Combine(DataDirectory, "notifications.json");
        public string StudiesFilePath => Path.Combine(DataDirectory, "studies.json");
        public string TasksFilePath => Path.Combine(DataDirectory, "tasks.json");
        public string UserCoursesFilePath => Path.Combine(DataDirectory, "userCourses.json");
        public string UsersExercisesProgressFilePath => Path.Combine(DataDirectory, "usersExercisesProgress.json");
        public string UsersFilePath => Path.Combine(DataDirectory, "users.json");
        public string UserTasksProgressFilePath => Path.Combine(DataDirectory, "userTasksProgress.json");

        private static string ResolveDataDirectory()
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory is not null)
            {
                if (string.Equals(
                    directory.Name,
                    "CheckmateAcademy.Presentation",
                    StringComparison.OrdinalIgnoreCase))
                {
                    var solutionDirectory = directory.Parent
                        ?? throw new DirectoryNotFoundException();

                    var infrastructureDirectory = Path.Combine(
                        solutionDirectory.FullName,
                        "CheckmateAcademy.Infrastructure");

                    if (!Directory.Exists(infrastructureDirectory))
                        throw new DirectoryNotFoundException(
                            $"Не найден проект Infrastructure: {infrastructureDirectory}");

                    var dataDirectory = Path.Combine(
                        infrastructureDirectory,
                        "Data");

                    Directory.CreateDirectory(dataDirectory);

                    return dataDirectory;
                }

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException(
                "Не удалось определить расположение проекта Presentation.");
        }
    }
}
