using CheckmateAcademy.Application.DataModels;
using CheckmateAcademy.Domain.ValueObjects.Content;

namespace CheckmateAcademy.Infrastructure.Mappers
{
    public static class ContentBlockMapper
    {
        public static ContentBlockData ToData(ContentBlock block)
        {
            ArgumentNullException.ThrowIfNull(block);

            return block switch
            {
                TextBlock textBlock => new ContentBlockData(
                    textBlock.Order,
                    null,
                    textBlock.Text
                ),

                ImageBlock imageBlock => new ContentBlockData(
                    imageBlock.Order,
                    imageBlock.ImageUrl,
                    null
                ),

                _ => throw new NotSupportedException(
                    $"Unsupported content block type: {block.GetType().Name}")
            };
        }

        public static ContentBlock ToDomain(ContentBlockData data)
        {
            ArgumentNullException.ThrowIfNull(data);

            if (data.Text is not null && data.ImageUrl is null)
                return new TextBlock(data.Text, data.Order);

            if (data.ImageUrl is not null && data.Text is null)
                return new ImageBlock(data.ImageUrl, data.Order);

            throw new ArgumentException(
                "Content block must contain either Text or ImageUrl.",
                nameof(data));
        }
    }
}
