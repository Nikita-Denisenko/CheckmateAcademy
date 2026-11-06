namespace CheckmateAcademy.Application.DataModels
{
    public record CourseData
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }

        public CourseData(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
