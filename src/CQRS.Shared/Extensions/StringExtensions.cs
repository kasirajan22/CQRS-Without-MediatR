using System.Net.Mail;

namespace CQRS.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        // Simple validation - adjust based on your requirements
        return phoneNumber.Length >= 10 && 
               phoneNumber.All(c => char.IsDigit(c) || c == '-' || c == '+' || c == ' ' || c == '(' || c == ')');
    }

    public static string ToTitleCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return char.ToUpper(input[0]) + input[1..].ToLower();
    }
}