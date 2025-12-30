public class FakeUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public void InsertUser(User user) => _users.Add(user);
    public User? GetByUsername(string username) => _users.SingleOrDefault(u => u.Username == username);
}
