namespace CheckmateAcademy.Domain.Exceptions
{
    public class DomainException : AppException
    {
        public DomainException(string message) : base(message) { }
    }
}
