namespace Application.Common.Exceptions;

public abstract class NotFoundException : ExceptionBase
{
    protected NotFoundException(string message)
        : base("Not Found", message)
    {
    }
}