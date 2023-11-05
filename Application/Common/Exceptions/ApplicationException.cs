namespace Application.Common.Exceptions;

public abstract class ExceptionBase : Exception
{
    protected ExceptionBase(string title, string message)
        : base(message)
    {
        Title = title;
    }

    public string Title { get; }
}
