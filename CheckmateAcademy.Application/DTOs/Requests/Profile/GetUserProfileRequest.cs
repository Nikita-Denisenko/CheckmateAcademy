namespace CheckmateAcademy.Application.DTOs.Requests.Profile
{
    public record GetUserProfileRequest
    {
        public int Id { get; init; }

        public GetUserProfileRequest(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            Id = id;
        }
    }
}
