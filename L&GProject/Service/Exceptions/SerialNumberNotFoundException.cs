namespace L_GProject.Service.Exceptions
{
    public class SerialNumberNotFoundException : Exception
    {
        public SerialNumberNotFoundException(string message) : base(message) { }
        public SerialNumberNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
