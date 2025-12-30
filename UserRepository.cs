using System.Data;
using System.Data.SqlClient;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    public UserRepository(string connectionString) => _connectionString = connectionString;

    public void InsertUser(User user)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO Users (Username, Email, PasswordHash, PasswordSalt, Role) VALUES (@u, @e, @ph, @ps, @r)", conn);

        cmd.Parameters.Add("@u", SqlDbType.VarChar, 100).Value = user.Username;
        cmd.Parameters.Add("@e", SqlDbType.VarChar, 100).Value = user.Email;
        cmd.Parameters.Add("@ph", SqlDbType.VarChar, 256).Value = user.PasswordHash;
        cmd.Parameters.Add("@ps", SqlDbType.VarChar, 64).Value = user.PasswordSalt;
        cmd.Parameters.Add("@r", SqlDbType.VarChar, 20).Value = user.Role;

        cmd.ExecuteNonQuery();
    }

    public User? GetByUsername(string username)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT UserID, Username, Email, PasswordHash, PasswordSalt, Role FROM Users WHERE Username = @u", conn);

        cmd.Parameters.Add("@u", SqlDbType.VarChar, 100).Value = username;

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        return new User
        {
            UserID = reader.GetInt32(0),
            Username = reader.GetString(1),
            Email = reader.GetString(2),
            PasswordHash = reader.GetString(3),
            PasswordSalt = reader.GetString(4),
            Role = reader.GetString(5)
        };
    }
}
