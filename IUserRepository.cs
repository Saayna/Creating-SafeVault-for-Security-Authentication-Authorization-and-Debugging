public interface IUserRepository
{
    void InsertUser(User user);
    User? GetByUsername(string username);
}
