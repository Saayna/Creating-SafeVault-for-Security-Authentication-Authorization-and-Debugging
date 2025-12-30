using System.Text.RegularExpressions;

public static class InputSanitizer
{
    private static readonly Regex UsernamePattern = new Regex("^[A-Za-z0-9._-]{3,100}$");

    public static string SanitizeUsername(string username)
    {
        if (!UsernamePattern.IsMatch(username))
            throw new ArgumentException("Invalid username format.");
        return username;
    }

    public static string SanitizeEmail(string email)
    {
        if (!new EmailAddressAttribute().IsValid(email))
            throw new ArgumentException("Invalid email format.");
        return email;
    }
}
