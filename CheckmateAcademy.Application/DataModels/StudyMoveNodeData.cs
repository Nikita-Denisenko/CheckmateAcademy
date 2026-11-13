namespace CheckmateAcademy.Application.DataModels
{
    public class StudyMoveNodeData
    {
        public int Id { get; set; }
        public StudyMoveData Move { get; set; } = null!;
        public List<StudyMoveNodeData> NextMoves { get; set; } = [];
    }
}
