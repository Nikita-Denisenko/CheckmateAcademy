namespace CheckmateAcademy.Application.DataModels
{
    public record SlideContentData
    {
        public List<ContentBlockData> ContentBlocks { get; init; } = [];

        public SlideContentData(List<ContentBlockData> contentBlocks)
        {
            ContentBlocks = contentBlocks;
        }
    }
}
