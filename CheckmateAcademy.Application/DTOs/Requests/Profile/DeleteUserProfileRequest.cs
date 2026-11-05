namespace CheckmateAcademy.Application.DTOs.Requests.Profile
{
    public record DeleteUserProfileRequest
    {
        public int Id { get; init; }

        public DeleteUserProfileRequest(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            Id = id;
        }
    }
}
