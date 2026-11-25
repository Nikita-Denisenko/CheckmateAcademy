using CheckmateAcademy.Application.Configuration;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Application.Interfaces.Services;
using CheckmateAcademy.Application.Services;
using CheckmateAcademy.Infrastructure.Repositories;
using CheckmateAcademy.Presentation.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheckmateAcademy.Presentation.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<AuthenticationOptions>(
                configuration.GetSection("Authentication"));

            services.AddSingleton<IBotRepository, BotRepository>();
            services.AddSingleton<ICourseRepository, CourseRepository>();
            services.AddSingleton<IExerciseRepository, ExerciseRepository>();
            services.AddSingleton<IFollowRepository, FollowRepository>();
            services.AddSingleton<IGameRepository, GameRepository>();
            services.AddSingleton<ILessonRepository, LessonRepository>();
            services.AddSingleton<INotificationRepository, NotificationRepository>();
            services.AddSingleton<IStudyRepository, StudyRepository>();
            services.AddSingleton<IChessTaskRepository, TaskRepository>();
            services.AddSingleton<IUserCourseRepository, UserCourseRepository>();
            services.AddSingleton<IUserExerciseProgressRepository, UserExerciseProgressRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserTaskProgressRepository, UserTaskProgressRepository>();

            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<IChessTaskService, ChessTaskService>();
            services.AddSingleton<ICourseService, CourseService>();
            services.AddSingleton<IExerciseService, ExerciseService>();
            services.AddSingleton<IFollowService, FollowService>();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<ITokenService, JwtTokenService>();
            services.AddSingleton<ILessonService, LessonService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IStudyService, StudyService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
