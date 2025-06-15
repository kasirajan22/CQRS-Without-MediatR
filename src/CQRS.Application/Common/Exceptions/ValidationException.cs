namespace CQRS.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<string> errors)
        : base("One or more validation failures have occurred.")
    {
        Errors = errors.ToList();
    }

    public ValidationException(string error)
        : base("Validation failure occurred.")
    {
        Errors = new List<string> { error };
    }

    public IReadOnlyList<string> Errors { get; }
}