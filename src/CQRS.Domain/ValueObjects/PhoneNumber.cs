using System.Text.RegularExpressions;

namespace CQRS.Domain.ValueObjects;

public class PhoneNumber
{
    private static readonly Regex PhoneRegex = new(
        @"^\+?[\d\s\-\(\)]{10,15}$",
        RegexOptions.Compiled);

    public string Value { get; private set; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be null or empty.", nameof(value));

        var cleanedValue = value.Trim();
        if (!PhoneRegex.IsMatch(cleanedValue))
            throw new ArgumentException("Invalid phone number format.", nameof(value));

        Value = cleanedValue;
    }

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    public static implicit operator PhoneNumber(string phoneNumber) => new(phoneNumber);

    public override string ToString() => Value;

    public override bool Equals(object? obj)
    {
        if (obj is PhoneNumber other)
            return Value == other.Value;
        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();
}