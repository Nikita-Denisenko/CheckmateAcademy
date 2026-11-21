namespace CheckmateAcademy.Application.DTOs.Requests.Chess
{
    public record CreatePieceSetupRequest
    {
        public string Type { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Row { get; set; }
        public int Column { get; set; }
        public bool HasMoved { get; set; }
    }
}
