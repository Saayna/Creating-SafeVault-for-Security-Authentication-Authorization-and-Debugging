using MySql.Data.MySqlClient;

public class MySqlUserRepository : IUserRepository
{
    private readonly string _connectionString;
    public MySqlUserRepository(string connectionString) => _connectionString = connectionString;

    public void InsertUser(User user)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        using var cmd = new MySqlCommand(
            "INSERT INTO Users (Username, Email, PasswordHash, PasswordSalt, Role) VALUES (@u, @e, @ph, @ps, @r)", conn);

        cmd.Parameters.AddWithValue("@u", user.Username);
        cmd.Parameters.AddWithValue("@e", user.Email);
        cmd.Parameters.AddWithValue("@ph", user.PasswordHash);
        cmd.Parameters.AddWithValue("@ps", user.PasswordSalt);
        cmd.Parameters.AddWithValue("@r", user.Role);

        cmd.ExecuteNonQuery();
    }

    public User? GetByUsername(string username)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();
        using var cmd = new MySqlCommand(
            "SELECT UserID, Username, Email, PasswordHash, PasswordSalt, Role FROM Users WHERE Username = @u", conn);

        cmd.Parameters.AddWithValue("@u", username);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        return new User
        {
            UserID = reader.GetInt32("UserID"),
            Username = reader.GetString("Username"),
            Email = reader.GetString("Email"),
            PasswordHash = reader.GetString("PasswordHash"),
            PasswordSalt = reader.GetString("PasswordSalt"),
            Role = reader.GetString("Role")
        };
    }
}
