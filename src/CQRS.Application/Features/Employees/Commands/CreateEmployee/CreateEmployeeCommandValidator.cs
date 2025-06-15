using CQRS.Shared.Constants;
using CQRS.Shared.Extensions;

namespace CQRS.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator
{
    public ValidationResult Validate(CreateEmployeeCommand command)
    {
        var errors = new List<string>();

        // First Name validation
        if (string.IsNullOrWhiteSpace(command.FirstName))
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.RequiredField, "First name"));
        else if (command.FirstName.Length > ApplicationConstants.Validation.MaxNameLength)
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.MaxLengthExceeded, "First name", ApplicationConstants.Validation.MaxNameLength));

        // Last Name validation
        if (string.IsNullOrWhiteSpace(command.LastName))
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.RequiredField, "Last name"));
        else if (command.LastName.Length > ApplicationConstants.Validation.MaxNameLength)
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.MaxLengthExceeded, "Last name", ApplicationConstants.Validation.MaxNameLength));

        // Gender validation
        if (!Enum.IsDefined(typeof(Domain.Enums.Gender), command.Gender))
            errors.Add("Invalid gender value");

        // Date of Birth validation
        if (command.DOB == default)
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.RequiredField, "Date of birth"));
        else if (!command.DOB.IsValidAge(ApplicationConstants.Validation.MinAge))
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.MinAgeRequired, ApplicationConstants.Validation.MinAge));

        // Email validation
        if (string.IsNullOrWhiteSpace(command.EmailID))
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.RequiredField, "Email"));
        else if (command.EmailID.Length > ApplicationConstants.Validation.MaxEmailLength)
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.MaxLengthExceeded, "Email", ApplicationConstants.Validation.MaxEmailLength));
        else if (!command.EmailID.IsValidEmail())
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.InvalidFormat, "email"));

        // Phone Number validation
        if (string.IsNullOrWhiteSpace(command.PhoneNo))
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.RequiredField, "Phone number"));
        else if (command.PhoneNo.Length > ApplicationConstants.Validation.MaxPhoneLength)
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.MaxLengthExceeded, "Phone number", ApplicationConstants.Validation.MaxPhoneLength));
        else if (!command.PhoneNo.IsValidPhoneNumber())
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.InvalidFormat, "phone number"));

        // Date of Joining validation
        if (command.DOJ == default)
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.RequiredField, "Date of joining"));
        else if (command.DOJ.IsInFuture())
            errors.Add(string.Format(ApplicationConstants.ErrorMessages.FutureDateNotAllowed, "Date of joining"));

        return new ValidationResult(errors);
    }
}

public class ValidationResult
{
    public ValidationResult(List<string> errors)
    {
        Errors = errors;
        IsValid = !errors.Any();
    }

    public bool IsValid { get; }
    public List<string> Errors { get; }
}