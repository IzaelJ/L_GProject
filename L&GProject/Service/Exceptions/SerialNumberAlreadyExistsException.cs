namespace L_GProject.Service.Exceptions
{
    public class SerialNumberAlreadyExistsException : Exception
    {
        public SerialNumberAlreadyExistsException(string message) : base(message) { }
        public SerialNumberAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }

    }
}
