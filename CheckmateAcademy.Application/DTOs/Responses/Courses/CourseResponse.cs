namespace CheckmateAcademy.Application.DTOs.Responses.Courses
{
    public record CourseResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int LessonsCount { get; init; }
        public int ReceivedPoints { get; init; }
        public int TotalPoints { get; init; }
        public int CompletionPercentage { get; init; }
        public bool IsCompleted { get; init; }

        public CourseResponse(
            int id,
            string name,
            string description,
            int lessonsCount,
            int receivedPoints,
            int totalPoints,
            int completionPercentage,
            bool isCompleted = false)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Id must be a positive integer.",
                    nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Name cannot be null or whitespace.",
                    nameof(name));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException(
                    "Description cannot be null or whitespace.",
                    nameof(description));
            }

            if (lessonsCount <= 0)
            {
                throw new ArgumentException(
                    "LessonsCount must be a positive integer.",
                    nameof(lessonsCount));
            }

            if (receivedPoints < 0)
            {
                throw new ArgumentException(
                    "ReceivedPoints cannot be negative.",
                    nameof(receivedPoints));
            }

            if (totalPoints < 0)
            {
                throw new ArgumentException(
                    "TotalPoints cannot be negative.",
                    nameof(totalPoints));
            }

            if (receivedPoints > totalPoints)
            {
                throw new ArgumentException(
                    "ReceivedPoints cannot exceed TotalPoints.",
                    nameof(receivedPoints));
            }

            if (completionPercentage < 0 || completionPercentage > 100)
            {
                throw new ArgumentException(
                    "CompletionPercentage must be between 0 and 100.",
                    nameof(completionPercentage));
            }

            if (isCompleted && completionPercentage < 100)
            {
                throw new ArgumentException(
                    "If the course is marked as completed, the CompletionPercentage must be 100.",
                    nameof(isCompleted));
            }

            Id = id;
            Name = name;
            Description = description;
            LessonsCount = lessonsCount;
            ReceivedPoints = receivedPoints;
            TotalPoints = totalPoints;
            CompletionPercentage = completionPercentage;
        }
    }
}
