using System.ComponentModel.DataAnnotations;

public class UserInput
{
    [Required, StringLength(100), RegularExpression("^[A-Za-z0-9._-]{3,100}$")]
    public string Username { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), StringLength(100, MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
}
