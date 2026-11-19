namespace CheckmateAcademy.Infrastructure.Configuration
{
    public class StorageOptions
    {
        public string DataDirectory { get; init; } = Path.Combine(
            AppContext.BaseDirectory,
            "Data");

        public string BotsFilePath => Path.Combine(DataDirectory, "bots.json");
    }
}
