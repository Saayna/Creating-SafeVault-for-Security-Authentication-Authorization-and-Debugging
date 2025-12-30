public class AuthService
{
    private readonly IUserRepository _repo;
    public AuthService(IUserRepository repo) => _repo = repo;

    public void Register(string username, string password, string role = "User")
    {
        var (hash, salt) = PasswordHasher.HashPassword(password);
        _repo.InsertUser(new User { Username = username, PasswordHash = hash, PasswordSalt = salt, Role = role });
    }

    public User? Authenticate(string username, string password)
    {
        var user = _repo.GetByUsername(username);
        if (user == null) return null;
        return PasswordHasher.Verify(password, user.PasswordHash, user.PasswordSalt) ? user : null;
    }

    public string GetRole(string username) => _repo.GetByUsername(username)?.Role ?? "Guest";
}
