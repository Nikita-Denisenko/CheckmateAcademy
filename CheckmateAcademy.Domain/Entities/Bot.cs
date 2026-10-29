
using CheckmateAcademy.Domain.Common;

namespace CheckmateAcademy.Domain.Entities
{
    public class Bot
    {
        public int Id { get; }
        public string Name { get; private set; }
        public int Rating { get; private set; }

        public Bot(int id, string name, int rating)
        {
            DomainValidation.PositiveId(id, nameof(id));
            DomainValidation.NotEmptyString(name, nameof(name));

            if (rating < 0 || rating > 3000)
                throw new ArgumentOutOfRangeException(
                    nameof(rating),
                    "Bot rating must be between 0 and 3000.");

            Id = id;
            Name = name;
            Rating = rating;
        }

        public void UpdateName(string name)
        {
            DomainValidation.NotEmptyString(name, nameof(name));
            Name = name;
        }

        public void UpdateRating(int rating)
        {
            if (rating < 0 || rating > 3000)
                throw new ArgumentOutOfRangeException(
                    nameof(rating),
                    "Bot rating must be between 0 and 3000.");

            Rating = rating;
        }
    }
}
