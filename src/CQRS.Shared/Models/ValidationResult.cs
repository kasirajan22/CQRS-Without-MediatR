namespace CQRS.Shared.Models;

public class ValidationResult
{
    public ValidationResult(List<string> errors)
    {
        Errors = errors ?? new List<string>();
        IsValid = !Errors.Any();
    }

    public ValidationResult(string error)
    {
        Errors = new List<string> { error };
        IsValid = false;
    }

    public ValidationResult()
    {
        Errors = new List<string>();
        IsValid = true;
    }

    public bool IsValid { get; }
    public List<string> Errors { get; }

    public static ValidationResult Success() => new();
    public static ValidationResult Failure(string error) => new(error);
    public static ValidationResult Failure(List<string> errors) => new(errors);
}