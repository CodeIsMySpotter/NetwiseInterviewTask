namespace WebApp.Exceptions;

public class AskRequestErrorException : Exception
{
    public AskRequestErrorException(string message) : base(message)
    {
    }
}
