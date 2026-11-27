using CheckmateAcademy.Application.DTOs.Requests.Account;
using CheckmateAcademy.Application.DTOs.Requests.Profile;
using CheckmateAcademy.Application.DTOs.Responses.Profile;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Application.Interfaces.Services;
using CheckmateAcademy.Domain.Entities;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.ValueObjects.Profile;

namespace CheckmateAcademy.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserCourseRepository _userCourseRepo;
        private readonly IUserTaskProgressRepository _userTaskProgressRepo;
        private readonly IFollowRepository _followRepo;
        private readonly IStudyRepository _studyRepo;
        private readonly IGameRepository _gameRepo;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IUserRepository userRepo,
            IUserCourseRepository userCourseRepo,
            IUserTaskProgressRepository userTaskProgressRepo,
            IFollowRepository followRepo,
            IStudyRepository studyRepo,
            IGameRepository gameRepo,
            IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo;
            _userCourseRepo = userCourseRepo;
            _userTaskProgressRepo = userTaskProgressRepo;
            _followRepo = followRepo;
            _studyRepo = studyRepo;
            _gameRepo = gameRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task ChangeEmailAsync(
            ChangeEmailRequest request,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await GetUserOrThrowAsync(request.UserId, ct);
            var email = new Email(request.NewEmail);

            var existingUser = await _userRepo.GetUserByEmailAsync(email, ct);

            if (existingUser is not null && existingUser.Id != user.Id)
                throw new InvalidOperationException("This email is already in use.");

            user.UpdateAccount(new UserAccount(
                email,
                user.Account.PasswordHash,
                user.Account.Role));

            await _userRepo.UpdateUserAsync(user, ct);
        }

        public async Task ChangePaswordAsync(
            ChangePasswordRequest request,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await GetUserOrThrowAsync(request.UserId, ct);

            if (!_passwordHasher.Verify(
                    request.CurrentPassword,
                    user.Account.PasswordHash))
            {
                throw new InvalidOperationException("Current password is incorrect.");
            }

            var passwordHash = _passwordHasher.Hash(request.NewPassword);

            user.UpdateAccount(new UserAccount(
                user.Account.Email,
                passwordHash,
                user.Account.Role));

            await _userRepo.UpdateUserAsync(user, ct);
        }

        public async Task DeleteAccoutAsync(
            DeleteAccountRequest request,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await GetUserOrThrowAsync(request.UserId, ct);

            var userCourses = await _userCourseRepo.GetAllUserCoursesAsync(ct);

            foreach (var userCourse in userCourses.Where(x => x.UserId == user.Id))
            {
                await _userCourseRepo.DeleteUserCourseAsync(
                    userCourse.UserId,
                    userCourse.CourseId,
                    ct);
            }

            var taskProgresses = await _userTaskProgressRepo
                .GetAllUserTaskProgressesAsync(ct);

            foreach (var progress in taskProgresses.Where(x => x.UserId == user.Id))
            {
                await _userTaskProgressRepo.DeleteUserTaskProgressAsync(
                    progress.UserId,
                    progress.TaskId,
                    ct);
            }

            var follows = await _followRepo.GetAllFollowsAsync(ct);

            foreach (var follow in follows.Where(
                         x => x.FollowerId == user.Id || x.TargetId == user.Id))
            {
                await _followRepo.DeleteFollowAsync(follow.Id, ct);
            }

            var studies = await _studyRepo.GetAllStudiesAsync(ct);

            foreach (var study in studies.Where(x => x.UserId == user.Id))
                await _studyRepo.DeleteStudyAsync(study.Id, ct);

            var games = await _gameRepo.GetAllGamesAsync(ct);

            foreach (var game in games.Where(x => x.UserId == user.Id))
                await _gameRepo.DeleteGameAsync(game.Id, ct);

            await _userRepo.DeleteUserAsync(user.Id, ct);
        }

        public async Task<UserProfileResponse> GetUserProfileAsync(
            GetUserProfileRequest request,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await GetUserOrThrowAsync(request.Id, ct);

            var follows = await _followRepo.GetAllFollowsAsync(ct);
            var userCourses = await _userCourseRepo.GetAllUserCoursesAsync(ct);
            var taskProgresses = await _userTaskProgressRepo
                .GetAllUserTaskProgressesAsync(ct);
            var games = await _gameRepo.GetAllGamesAsync(ct);
            var studies = await _studyRepo.GetAllStudiesAsync(ct);

            var followersCount = follows.Count(x => x.TargetId == user.Id);

            var totalPoints = userCourses
                .Where(x => x.UserId == user.Id)
                .Sum(x => x.Progress.ReceivedPoints);

            var solvedTasksCount = taskProgresses.Count(
                x => x.UserId == user.Id &&
                     x.Status == TaskProgressStatus.Completed);

            var gamesQuantity = games.Count(x => x.UserId == user.Id);
            var studiesQuantity = studies.Count(x => x.UserId == user.Id);

            return new UserProfileResponse(
                user.Id,
                user.Profile.Name,
                user.Profile.Surname,
                user.Profile.BirthDate,
                user.Profile.AboutInfo,
                user.Profile.AvatarUrl,
                user.Profile.FideRating,
                user.Profile.RusRating,
                followersCount,
                totalPoints,
                solvedTasksCount,
                gamesQuantity,
                studiesQuantity);
        }

        public async Task<UserSettingsResponse> GetUserSettingsResponseAsync(
            GetUserSettingsRequest request,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await GetUserOrThrowAsync(request.UserId, ct);

            return new UserSettingsResponse
            {
                Email = user.Account.Email.Value
            };
        }

        public async Task<UserProfileResponse> UpdateUserProfileAsync(
            UpdateUserProfileRequest request,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await GetUserOrThrowAsync(request.Id, ct);

            var profile = new UserProfile(
                request.Name,
                request.Surname,
                request.BirthDate,
                request.AboutInfo,
                request.AvatarUrl,
                request.FideRating,
                request.RusRating);

            user.UpdateProfile(profile);

            await _userRepo.UpdateUserAsync(user, ct);

            return await GetUserProfileAsync(
                new GetUserProfileRequest(user.Id),
                ct);
        }

        private async Task<User> GetUserOrThrowAsync(
            int userId,
            CancellationToken ct)
        {
            return await _userRepo.GetUserByIdAsync(userId, ct)
                ?? throw new KeyNotFoundException(
                    $"User with id {userId} not found.");
        }
    }
}
