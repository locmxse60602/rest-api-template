namespace Application.Common.Exceptions;
public abstract class BadRequestException : ExceptionBase
{
    protected BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}