namespace CheckmateAcademy.Application.DataModels;

public record PieceSetupData
{
    public string Type { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int Row { get; set; }
    public int Column { get; set; }
}
