namespace WebApp.Core.Exceptions;

public class AskRequestErrorException : Exception
{
    public AskRequestErrorException(string message) : base(message)
    {
    }
}
