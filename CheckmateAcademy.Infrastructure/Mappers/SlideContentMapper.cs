using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.ValueObjects.Content;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class SlideContentMapper
    {
        public static SlideContentData ToData(SlideContent content)
        {
            ArgumentNullException.ThrowIfNull(content);

            return new SlideContentData(
                content.Blocks
                    .Select(ContentBlockMapper.ToData)
                    .ToList()
            );
        }

        public static SlideContent ToDomain(SlideContentData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            return new SlideContent(
                data.ContentBlocks
                    .Select(ContentBlockMapper.ToDomain)
            );
        }
    }
}
