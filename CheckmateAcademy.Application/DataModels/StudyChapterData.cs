namespace CheckmateAcademy.Application.DataModels
{
    public class StudyChapterData
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Order { get; set; }
        public ChessBoardSetupData InitialPosition { get; set; } = null!;
        public List<StudyMoveNodeData> Moves { get; set; } = [];
    }
}