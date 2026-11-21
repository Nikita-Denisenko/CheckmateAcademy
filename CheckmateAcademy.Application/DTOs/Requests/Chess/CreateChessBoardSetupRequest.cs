namespace CheckmateAcademy.Application.DTOs.Requests.Chess
{
    public record CreateChessBoardSetupRequest
    {
        public List<CreatePieceSetupRequest> Pieces { get; set; } = [];
    }
}
