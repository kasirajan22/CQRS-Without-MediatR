namespace CQRS.Shared.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthDate.Year;
        
        if (birthDate > today.AddYears(-age))
            age--;
            
        return age;
    }

    public static int CalculateYearsOfService(this DateOnly joinDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var years = today.Year - joinDate.Year;
        
        if (joinDate > today.AddYears(-years))
            years--;
            
        return Math.Max(0, years);
    }

    public static bool IsInFuture(this DateOnly date)
    {
        return date > DateOnly.FromDateTime(DateTime.Today);
    }

    public static bool IsValidAge(this DateOnly birthDate, int minimumAge = 16)
    {
        return birthDate.CalculateAge() >= minimumAge;
    }
}