using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.Entities;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class ContentSlideMapper
    {
        public static LessonSlideData ToData(ContentSlide slide)
        {
            ArgumentNullException.ThrowIfNull(slide);

            return new LessonSlideData(
                slide.Title,
                slide.Order,
                slide.CreatedAt,
                SlideContentMapper.ToData(slide.Content),
                null
            );
        }

        public static ContentSlide ToDomain(LessonSlideData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            if (data.SlideContentData is null || data.ExerciseData is not null)
                throw new ArgumentException(
                    "Invalid data for content slide.",
                    nameof(data));

            return new ContentSlide(
                data.Title,
                data.Order,
                data.CreatedAt,
                SlideContentMapper.ToDomain(data.SlideContentData)
            );
        }
    }
}
