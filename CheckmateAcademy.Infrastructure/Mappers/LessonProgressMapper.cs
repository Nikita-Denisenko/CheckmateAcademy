using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.ValueObjects.Courses;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class LessonProgressMapper
    {
        public static LessonProgressData ToData(LessonProgress progress)
        {
            ArgumentNullException.ThrowIfNull(progress);

            return new LessonProgressData(
                progress.LessonId,
                progress.ReceivedPoints,
                progress.TotalPoints,
                progress.CompletedSlides,
                progress.TotalSlides
            );
        }

        public static LessonProgress ToDomain(LessonProgressData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return new LessonProgress(
                data.LessonId,
                data.ReceivedPoints,
                data.TotalPoints,
                data.TotalSlides,
                data.CompletedSlides
            );
        }
    }
}
