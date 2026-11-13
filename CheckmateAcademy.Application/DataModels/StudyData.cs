namespace CheckmateAcademy.Application.DataModels
{
    public class StudyData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public List<StudyChapterData> Chapters { get; set; } = [];
    }
}