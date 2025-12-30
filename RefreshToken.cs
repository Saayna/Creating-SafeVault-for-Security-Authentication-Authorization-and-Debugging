public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public bool Revoked { get; set; }
}
