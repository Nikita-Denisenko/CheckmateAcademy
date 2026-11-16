using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class ExerciseSlideMapper
    {
        public static LessonSlideData ToData(ExerciseSlide slide)
        {
            ArgumentNullException.ThrowIfNull(slide);

            return new LessonSlideData(
                slide.Title,
                slide.Order,
                slide.CreatedAt,
                null,
                ExerciseMapper.ToData(slide.Exercise));
        }

        public static ExerciseSlide ToDomain(LessonSlideData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            if (data.ExerciseData is null)
                throw new ArgumentException(
                    "ExerciseData cannot be null.",
                    nameof(data));

            if (data.SlideContentData is not null)
                throw new ArgumentException(
                    "Exercise slide cannot contain content slide data.",
                    nameof(data));

            return new ExerciseSlide(
                data.Title,
                data.Order,
                data.CreatedAt,
                ExerciseMapper.ToDomain(data.ExerciseData));
        }
    }
}
