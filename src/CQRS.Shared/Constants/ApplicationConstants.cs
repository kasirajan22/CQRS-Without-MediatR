namespace CQRS.Shared.Constants;

public static class ApplicationConstants
{
    public static class Validation
    {
        public const int MaxNameLength = 50;
        public const int MaxEmailLength = 100;
        public const int MaxPhoneLength = 20;
        public const int MaxSummaryLength = 100;
        public const int MinAge = 16;
    }

    public static class ErrorMessages
    {
        public const string RequiredField = "{0} is required";
        public const string MaxLengthExceeded = "{0} cannot exceed {1} characters";
        public const string InvalidFormat = "Invalid {0} format";
        public const string NotFound = "{0} with ID {1} was not found";
        public const string MinAgeRequired = "Employee must be at least {0} years old";
        public const string FutureDateNotAllowed = "{0} cannot be in the future";
    }

    public static class Gender
    {
        public const string Male = "Male";
        public const string Female = "Female";
        public const string Other = "Other";
    }
}