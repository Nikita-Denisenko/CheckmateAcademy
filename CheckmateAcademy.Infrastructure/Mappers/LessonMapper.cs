using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class LessonMapper
    {
        public static LessonData ToData(Lesson lesson, int courseId)
        {
            ArgumentNullException.ThrowIfNull(lesson);

            return new LessonData(
                lesson.Id,
                courseId,
                lesson.Title,
                lesson.Description,
                lesson.Order,
                lesson.Slides
                    .Select(MapSlideToDataModel)
                    .ToList()
            );
        }

        public static Lesson ToDomain(LessonData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return new Lesson(
                data.Id,
                data.Title,
                data.Description,
                data.Order,
                data.Slides.Select(MapSlideToDomainEntity)
            );
        }

        private static LessonSlideData MapSlideToDataModel(LessonSlide slide)
        {
            return slide switch
            {
                ContentSlide contentSlide =>
                    ContentSlideMapper.ToData(contentSlide),

                ExerciseSlide exerciseSlide =>
                    ExerciseSlideMapper.ToData(exerciseSlide),

                _ => throw new NotSupportedException(
                    $"Unsupported slide type: {slide.GetType().Name}")
            };
        }

        private static LessonSlide MapSlideToDomainEntity(LessonSlideData data)
        {
            if (data.SlideContentData is not null && data.ExerciseData is null)
                return ContentSlideMapper.ToDomain(data);

            if (data.ExerciseData is not null && data.SlideContentData is null)
                return ExerciseSlideMapper.ToDomain(data);

            throw new ArgumentException(
                "Lesson slide must contain either SlideContentData or ExerciseData.",
                nameof(data));
        }
    }
}
