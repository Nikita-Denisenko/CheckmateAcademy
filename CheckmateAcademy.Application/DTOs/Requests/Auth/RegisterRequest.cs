namespace CheckmateAcademy.Application.DTOs.Requests.Auth
{
    public record RegisterRequest
    {
        public string Name { get; }
        public string Surname { get; }
        public DateTime BirthDate { get; }
        public string Email { get; }
        public string Password { get; }

        public RegisterRequest(string name, string surname, DateTime birthDate, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Surname cannot be null or empty.", nameof(surname));

            if (birthDate == default)
                throw new ArgumentException("Birth date cannot be the default value.", nameof(birthDate));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Email = email;
            Password = password;
        }
    }
}
