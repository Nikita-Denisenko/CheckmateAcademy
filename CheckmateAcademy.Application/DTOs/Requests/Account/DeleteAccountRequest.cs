namespace CheckmateAcademy.Application.DTOs.Requests.Account
{
    public record DeleteAccountRequest
    {
        public int UserId { get; init; }

        public DeleteAccountRequest(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(userId));

            UserId = userId;
        }
    }
}
