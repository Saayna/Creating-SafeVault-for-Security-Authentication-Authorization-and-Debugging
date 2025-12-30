[TestFixture]
public class AuthServiceTests
{
    private FakeUserRepository _repo = null!;
    private AuthService _auth = null!;

    [SetUp]
    public void Setup()
    {
        _repo = new FakeUserRepository();
        _auth = new AuthService(_repo);
    }

    [Test]
    public void Register_ShouldStoreHashedPassword()
    {
        _auth.Register("alice", "StrongPass!123");
        var user = _repo.GetByUsername("alice");
        Assert.IsNotNull(user);
        Assert.AreNotEqual("StrongPass!123", user!.PasswordHash);
    }
}
