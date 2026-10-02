using System.Text.RegularExpressions;

namespace CheckmateAcademy.Domain.ValueObjects.Profile
{
    public sealed record Email
    {
        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(
                    "Email cannot be null or whitespace.",
                    nameof(value));

            if (!EmailRegex.IsMatch(value))
                throw new ArgumentException(
                    "Invalid email format.",
                    nameof(value));

            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
