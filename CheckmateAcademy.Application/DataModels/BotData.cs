namespace CheckmateAcademy.Application.DataModels
{
    public record BotData
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Rating { get; init; }
    }
}
