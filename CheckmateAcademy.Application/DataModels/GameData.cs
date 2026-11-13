namespace CheckmateAcademy.Application.DataModels
{
    public class GameData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BotId { get; set; }

        public ChessBoardSetupData InitialPosition { get; set; } = null!;

        public List<GameMoveData> Moves { get; set; } = [];
    }
}
