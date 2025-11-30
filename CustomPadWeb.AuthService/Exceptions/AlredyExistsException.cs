namespace CustomPadWeb.AuthService.Exceptions
{
    public class AlredyExistsException : Exception
    {
        public AlredyExistsException(string message) : base(message)
        { }
    }
}
