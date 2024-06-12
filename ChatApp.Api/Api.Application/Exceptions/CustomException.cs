namespace Api.Application.Exceptions;

public class CustomException : Exception
{
    public CustomException() : base()
    {
        
    }

    public CustomException(string Message) :base(Message)
    {
        
    }

    public CustomException(string Message, params object[] args) : base(string.Format(Message, args))
    {
        
    }
}
